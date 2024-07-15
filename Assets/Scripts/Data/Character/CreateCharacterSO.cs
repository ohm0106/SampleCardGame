using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System;

public class CreateCharacterSO
{
    private static readonly string jsonFilePath = "Assets/Character.json";

    [MenuItem("Assets/Character/Load Character Data from JSON")]
    public static void LoadCharacterDataFromJSON()
    {
        if (File.Exists(jsonFilePath))
        {
            string jsonData = File.ReadAllText(jsonFilePath);
            CharacterDataList characterDataList = JsonUtility.FromJson<CharacterDataList>(jsonData);

            foreach (var characterData in characterDataList.characters)
            {
                CharacterBaseSO newCharacter = ScriptableObject.CreateInstance<CharacterBaseSO>();
                newCharacter.name = characterData.name;
                newCharacter.type = (CharacterType)Enum.Parse(typeof(CharacterType), characterData.type);
                newCharacter.spritePath = characterData.spritePath;
                newCharacter.gradeType = (GradeType)Enum.Parse(typeof(GradeType), characterData.gradeType);
                newCharacter.stats = new List<Stat>();
                foreach (var stat in characterData.stats)
                {
                    if (!string.IsNullOrEmpty(stat.statType))
                    {
                        Stat t = new Stat
                        {
                            statType = (StatType)Enum.Parse(typeof(StatType), stat.statType, true), // true to ignore case
                            value = stat.value
                        };
                        newCharacter.stats.Add(t);
                    }
                    else
                    {
                        Debug.LogWarning($"Invalid stat type for character {characterData.name}");
                    }
                }
                newCharacter.skills = characterData.skills;
                newCharacter.defaultCost = characterData.defaultCost;
                newCharacter.defaultUpgradeCost = characterData.defaultUpgradeCost;
                newCharacter.description = characterData.description;

                AssetDatabase.CreateAsset(newCharacter, $"Assets/SO/Characters/{newCharacter.name}.asset");
            }

            AssetDatabase.SaveAssets();
        }
        else
        {
            Debug.LogError($"JSON file not found at path: {jsonFilePath}");
        }
    }

    [MenuItem("Assets/Character/Delete All CharacterSO Data")]
    public static void DeleteAllCharacters()
    {
        string[] characterPaths = Directory.GetFiles("Assets/SO/Characters", "*.asset");
        foreach (string characterPath in characterPaths)
        {
            AssetDatabase.DeleteAsset(characterPath);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

}

[Serializable]
public class CharacterDataList
{
    public List<CharacterData> characters;
}

[Serializable]
public class CharacterData
{
    public string name;
    public string type;
    public string spritePath;
    public string gradeType;
    public List<StatData> stats;
    public List<Skill> skills;
    public int defaultCost;
    public int defaultUpgradeCost;
    public string description;
}

[Serializable]
public class StatData
{
    public string statType;
    public int value;
}
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class CharacterCollectionDebugger : MonoBehaviour
{
    [SerializeField]
    private CharacterCollection characterCollection;

    public string selectedCharacterBaseSOName;
    private int newCharacterStar = 0;
    private int newCharacterLevel = 0;
    public int newCharacterDeckCount;
    public List<Stat> newCharacterStats = new List<Stat>();
    public List<Rune> newCharacterRunes = new List<Rune>();


    private void OnValidate()
    {
        if (characterCollection == null)
        {
            characterCollection = FindObjectOfType<CharacterCollection>();
        }
    }

    public void AddCharacter()
    {
        string[] guids = AssetDatabase.FindAssets("t:CharacterBaseSO", new[] { "Assets/SO/Characters" });
        List<CharacterBaseSO> characterBaseList = guids.Select(guid => AssetDatabase.LoadAssetAtPath<CharacterBaseSO>(AssetDatabase.GUIDToAssetPath(guid))).ToList();
        CharacterBaseSO selectedCharacterBaseSO = characterBaseList.Find(c => c.name == selectedCharacterBaseSOName);

        if (selectedCharacterBaseSO != null)
        {
            Character newCharacter = new Character
            {
                name = selectedCharacterBaseSO.name,
                star = newCharacterStar,
                Level = newCharacterLevel,
                stats = newCharacterStats.Count > 0 ? newCharacterStats : selectedCharacterBaseSO.stats,
                runes = newCharacterRunes
            };

            characterCollection.AddCharacter(newCharacter);
        }
        else
        {
            Debugger.PrintLog("Selected character base SO not found.", LogType.Warning);
        }
    }

    public void RemoveCharacter(string id)
    {
        characterCollection.RemoveCharacter(id);
    }

    public void SaveCharacters()
    {
        if (!string.IsNullOrEmpty(SaveLoadUtility.deckFilePath))
        {
            DeckData curDeckData = new DeckData
            {
                characters = characterCollection.GetCharacterList()
            };

            SaveLoadUtility.SaveData(curDeckData, SaveLoadUtility.deckFilePath);
        }
        else
        {
            Debugger.PrintLog("No DeckData selected to save.",LogType.Warning);
        }
    }

    public void LoadCharacters()
    {
        if (!string.IsNullOrEmpty(SaveLoadUtility.deckFilePath))
        {
            DeckData loadedDeckData = SaveLoadUtility.LoadData<DeckData>(SaveLoadUtility.deckFilePath);
            characterCollection.SetCharactersFromList(loadedDeckData);
        }
        else
        {
            Debugger.PrintLog("No DeckData selected to load.", LogType.Warning);
        }
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollection : MonoBehaviour
{
    public Dictionary<string, Character> characters;

    public event Action<Character> onRemoveCharacter;
    public event Action<Character> onAddCharacter;

    private void Start()
    {
        DeckData loadDeckData = SaveLoadUtility.LoadData<DeckData>(SaveLoadUtility.deckFilePath);

        if (loadDeckData == null)
            loadDeckData = new DeckData();

        SetCharactersFromList(loadDeckData);

    }
    private void OnDestroy()
    {
        DeckData curDeckData = SaveLoadUtility.LoadData<DeckData>(SaveLoadUtility.deckFilePath);
        curDeckData.characters = GetCharacterList();

        SaveLoadUtility.SaveData<DeckData>(curDeckData, SaveLoadUtility.deckFilePath);
    }
    public void AddCharacter(Character newCharacter)
    {
        if (!characters.ContainsKey(newCharacter.GetGUID()))
        {
            characters[newCharacter.GetGUID()] = newCharacter;
            onAddCharacter?.Invoke(newCharacter);
        }
    }


    public void RemoveCharacter(string id)
    {
        if (characters.ContainsKey(id))
        {
            characters.TryGetValue(id, out Character character);
            characters.Remove(id);
            onRemoveCharacter?.Invoke(character);
        }
    }

    List<Character> GetCharacterList()
    {
        return new List<Character>(characters.Values);
    }
    void SetCharactersFromList(DeckData deckData)
    {
        characters.Clear();
        foreach (var character in deckData.characters)
        {
            characters[character.GetGUID()] = character;
        }
    }
}

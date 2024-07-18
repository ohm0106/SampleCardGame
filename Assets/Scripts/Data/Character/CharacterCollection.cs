using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollection : MonoBehaviour
{
    public Dictionary<string, Character> decks = new Dictionary<string, Character>();

    public event Action<Character> onRemoveCharacter;
    public event Action<Character> onAddCharacter;

    // todo ·é, ½ºÅÝ 

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
        if (!decks.ContainsKey(newCharacter.GetGUID()))
        {
            decks[newCharacter.GetGUID()] = newCharacter;
            onAddCharacter?.Invoke(newCharacter);
        }
    }

    public void RemoveCharacter(string id)
    {
        if (decks.ContainsKey(id))
        {
            decks.TryGetValue(id, out Character character);
            decks.Remove(id);
            onRemoveCharacter?.Invoke(character);
        }
    }

    public void GetCharacter(string id)
    {
        if (decks.ContainsKey(id))
        {
            decks.TryGetValue(id, out Character character);
        }
    }

    public List<Character> GetCharacterList()
    {
        return new List<Character>(decks.Values);
    }

    public void SetCharactersFromList(DeckData deckData)
    {
        decks.Clear();

        if (deckData.characters.Count == 0)
            return;

        foreach (var character in deckData.characters)
        {
            decks[character.GetGUID()] = character;
        }
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollection : MonoBehaviour
{
    Dictionary<string, Character> decks = new Dictionary<string, Character>();
    PlayerDecks deck_Info; 
    public event Action<Character> onRemoveCharacter;
    public event Action<Character> onAddCharacter;

    public event Action<string> onRemoveDeckCount;
    public event Action<string> onAddDeckCount;

    // todo ·é, ½ºÅÝ 

    private void Awake()
    {
        DeckData loadDeckData = SaveLoadUtility.LoadData<DeckData>(SaveLoadUtility.deckFilePath);
        deck_Info = SaveLoadUtility.LoadData<PlayerDecks>(SaveLoadUtility.deckFilePath);

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

    public void RemoveAllCharacters()
    {
        var characterList = GetCharacterList();
        decks.Clear();
        foreach (var character in characterList)
        {
            onRemoveCharacter?.Invoke(character);
        }
    }

    public Character GetCharacter(string id)
    {
        if (decks.ContainsKey(id))
        {
            decks.TryGetValue(id, out Character character);
            return character;
        }
        return null; 
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

    public PlayerDecks GetPlayerDeckInfo()
    {
        return deck_Info;
    }
    public void RemovePlayerDeck(int count, string id)
    {
        if (deck_Info.info.Length >= count && !deck_Info.info[count].id.Equals("-1"))
        {
            deck_Info.info[count].id = id;
        }
    }

    public void ReleaseLock(int count)
    {
        if (deck_Info.info.Length >= count && deck_Info.info[count].id.Equals("-1"))
        {
            deck_Info.info[count].id = "0";
        }
    }
}

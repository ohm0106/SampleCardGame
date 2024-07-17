using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Character
{
    public string id;

    public string name;

    public int star;

    public int Level;

    public List<Stat> stats;

    public List<Rune> runes;

    public Character()
    {
        id = Guid.NewGuid().ToString();
        stats = new List<Stat>();
        runes = new List<Rune>();
    }

    public string GetGUID()
    {
        return id;
    }
}

[Serializable]
public class DeckData
{
    // ������ �ִ� ĳ���� 
    public List<Character> characters;

    // ������ �ִ� ��


    public List<Character> GetCharacterList()
    {
        if (characters.Count == 0)
            return new List<Character>();

        return characters;
    }

    public void SetCharactersFromList(List<Character> characterList)
    {
        characters = characterList;
    }
}
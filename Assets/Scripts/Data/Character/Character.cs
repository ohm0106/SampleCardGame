using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Character
{
    string id;

    public string name;

    public int star;

    public int Level;

    public int deckCount;

    public List<Stat> stats;

    public List<Rune> runes;

    public int curUpgradeCost;

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
}
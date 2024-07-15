using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterBaseData", menuName = "ScriptableObjects/CharacterBaseData", order = 1)]
public class CharacterBaseSO : ScriptableObject
{
    public string name;
    public CharacterType type;
    public string spritePath;
    public GradeType gradeType;
    public List<Stat> stats;
    public List<Skill> skills;
    public int defaultCost;
    public int defaultUpgradeCost;
    public string description;
}


public enum CharacterType
{
    Sniper,
    Tanker,
    Terrorist,
    Knight
}


[Serializable]
public class Skill
{
    public string skillName;
    public int skillLevel;
}

[Serializable]
public class Rune
{
    public string runeName;
    public int runeLevel;
}

[Serializable]
public class Character
{
    public string name;
    public int star;
    
    public int Level;
    public int LevelAmount;

    public List<Stat> stats;

    public int runePossibleCount;
    public int runMaxCount;
    public List<Rune> runes;

    public int curCost;
    public int curUpgradeCost;
}
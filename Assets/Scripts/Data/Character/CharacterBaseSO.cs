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

//TODO ; SKill ¹× Rune ¼³Á¤
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


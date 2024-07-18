using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public int price;
    public ItemType itemType;
    public GradeType gradeType;

    [Header("Stats")]
    public Stat[] stats;

    public Sprite icon;
    public string description;
}

[Flags]
public enum ItemType
{
    None = 0,
    Weapon = 1 << 0,    
    Shield = 1 << 1,     
    Armor = 1 << 2,     
    Accessory = 1 << 3, 
    Potion = 1 << 4,     
    Resource = 1 << 5,
    All = int.MaxValue
}

public enum StatType
{
    Critical,
    Defense,
    Health,
    AttackDamage,
    MoveSpeed
}

public enum GradeType
{
    COMMON,
    EPIC,
    RARE,
    LEGENDARY,
    NONE
}

[System.Serializable]
public class Stat
{
    public StatType statType;
    public int value;
}
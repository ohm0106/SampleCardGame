using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public int price;
    public ItemType itemType;
    public GradeType gradeType;
    // Stat
    public int defense;
    public int critical;
    public Sprite icon;
    public string description;
}

public enum ItemType
{
    Weapon,
    Shield,
    Armor,
    Accessory,
    Potion,
    Resource
}

public enum GradeType
{
    COMMON,
    EPIC,
    RARE,
    LEGENDARY
}
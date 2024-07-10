using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public class CreateItemSO
{
    public static List<ItemSO> items = new List<ItemSO>
    {
         CreateItem("Bomb_Bomb", 300, ItemType.Weapon, GradeType.EPIC, 0, 10, "Bomb icon."),
         CreateItem("Bomb_Dynamite", 800, ItemType.Weapon, GradeType.EPIC, 0, 10, "Bomb icon."),
         CreateItem("Boots", 200, ItemType.Armor, GradeType.COMMON, 5, 0, "A pair of sturdy boots."),
         CreateItem("BoxingGloves", 250, ItemType.Weapon, GradeType.COMMON, 0, 3, "Boxing gloves for combat."),
         CreateItem("Clover", 150, ItemType.Accessory, GradeType.COMMON, 0, 0, "A clover that brings good luck."),
         CreateItem("Crown", 500, ItemType.Accessory, GradeType.LEGENDARY, 5, 0, "A crown fit for a king."),
         CreateItem("DogGum", 30, ItemType.Resource, GradeType.COMMON, 0, 0, "A bone for dogs."),
         CreateItem("Egg", 10, ItemType.Resource, GradeType.COMMON, 0, 0, "A simple egg."),
         CreateItem("Emergency_Bag", 100, ItemType.Resource, GradeType.COMMON, 0, 0, "Emergency supplies."),
         CreateItem("Flippers", 120, ItemType.Accessory, GradeType.COMMON, 0, 0, "Flippers for swimming."),
         CreateItem("Food_Can", 20, ItemType.Resource, GradeType.COMMON, 0, 0, "A can of food."),
         CreateItem("Food_Pizza", 40, ItemType.Resource, GradeType.COMMON, 0, 0, "A slice of pizza."),
         CreateItem("Food_Shell", 50, ItemType.Resource, GradeType.COMMON, 0, 0, "A seafood shell."),
         CreateItem("Hammer", 150, ItemType.Weapon, GradeType.COMMON, 0, 3, "A basic hammer."),
         CreateItem("Horsesheos", 70, ItemType.Resource, GradeType.COMMON, 0, 0, "A lucky horseshoe."),
         CreateItem("Magnetic", 80, ItemType.Resource, GradeType.COMMON, 0, 0, "A strong magnet."),
         CreateItem("Mail", 100, ItemType.Resource, GradeType.COMMON, 0, 0, "A mail envelope."),
         CreateItem("Missile", 400, ItemType.Weapon, GradeType.EPIC, 0, 15, "A powerful missile."),
         CreateItem("Nut", 30, ItemType.Resource, GradeType.COMMON, 0, 0, "A basic nut."),
         CreateItem("Oil", 100, ItemType.Resource, GradeType.COMMON, 0, 0, "A bottle of oil."),
         CreateItem("Potion_Blue", 150, ItemType.Potion, GradeType.COMMON, 0, 0, "A blue potion."),
         CreateItem("Potion_Purple", 150, ItemType.Potion, GradeType.COMMON, 0, 0, "A purple potion."),
         CreateItem("Potion_Red", 150, ItemType.Potion, GradeType.COMMON, 0, 0, "A red potion."),
         CreateItem("BigPotion_Green", 150, ItemType.Potion, GradeType.COMMON, 0, 0, "A green potion."),
         CreateItem("BigPotion_Purple", 150, ItemType.Potion, GradeType.COMMON, 0, 0, "A purple potion."),
         CreateItem("BigPotion_Red", 150, ItemType.Potion, GradeType.COMMON, 0, 0, "A yellow potion."),
         CreateItem("Pumkin", 200, ItemType.Resource, GradeType.COMMON, 0, 0, "A Halloween pumpkin."),
         CreateItem("Shield", 400, ItemType.Shield, GradeType.RARE, 10, 0, "A sturdy shield."),
         CreateItem("Shovel", 100, ItemType.Resource, GradeType.COMMON, 0, 0, "A tool for digging."),
         CreateItem("Sword", 300, ItemType.Weapon, GradeType.RARE, 0, 7, "A sharp sword."),
         CreateItem("Tooth", 50, ItemType.Resource, GradeType.COMMON, 0, 0, "A tooth.")
    };

    [MenuItem("Assets/Item/Create ItemSO Data")]
    public static void CreateItems()
    {
        foreach (var item in items)
        {
            AssetDatabase.CreateAsset(item, $"Assets/SO/Items/{item.itemName}.asset");
        }

        AssetDatabase.SaveAssets();
    }

    [MenuItem("Assets/Item/Delete All ItemSO Data")]
    public static void DeleteAllItems()
    {
        string[] itemPaths = Directory.GetFiles("Assets/SO/Items", "*.asset");
        foreach (string itemPath in itemPaths)
        {
            AssetDatabase.DeleteAsset(itemPath);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
    private static ItemSO CreateItem(string name, int price, ItemType itemType, GradeType gradeType, int defense, int critical, string description)
    {
        ItemSO newItem = ScriptableObject.CreateInstance<ItemSO>();
        newItem.itemName = name;
        newItem.price = price;
        newItem.itemType = itemType;
        newItem.gradeType = gradeType;
        newItem.defense = defense;
        newItem.critical = critical;
        newItem.icon = Resources.Load<Sprite>($"ItemIcons/Icon_{name}");
        newItem.description = description;
        return newItem;
    }
}

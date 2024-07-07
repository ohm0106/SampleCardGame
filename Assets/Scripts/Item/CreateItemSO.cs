using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public class CreateItemSO
{
    [MenuItem("Assets/Item/Create ItemSO Data")]
    public static void CreateItems()
    {
        // 아이템 데이터를 담을 리스트
        List<ItemSO> items = new List<ItemSO>();

        // 아이템 데이터를 추가합니다.
        items.Add(CreateItem("Bomb", 300, ItemType.Weapon, GradeType.EPIC, 0, 10, "Bomb icon."));
        items.Add(CreateItem("Boots", 200, ItemType.Armor, GradeType.COMMON, 5, 0, "A pair of sturdy boots."));
        items.Add(CreateItem("BoxingGlove", 250, ItemType.Weapon, GradeType.COMMON, 0, 3, "Boxing gloves for combat."));
        items.Add(CreateItem("Clover", 150, ItemType.Accessory, GradeType.COMMON, 0, 0, "A clover that brings good luck."));
        items.Add(CreateItem("Crown", 500, ItemType.Accessory, GradeType.LEGENDARY, 5, 0, "A crown fit for a king."));
        items.Add(CreateItem("DogBone", 30, ItemType.Resource, GradeType.COMMON, 0, 0, "A bone for dogs."));
        items.Add(CreateItem("Egg", 10, ItemType.Resource, GradeType.COMMON, 0, 0, "A simple egg."));
        items.Add(CreateItem("Emergency", 100, ItemType.Resource, GradeType.COMMON, 0, 0, "Emergency supplies."));
        items.Add(CreateItem("Flippers", 120, ItemType.Accessory, GradeType.COMMON, 0, 0, "Flippers for swimming."));
        items.Add(CreateItem("FoodCan", 20, ItemType.Resource, GradeType.COMMON, 0, 0, "A can of food."));
        items.Add(CreateItem("FoodChicken", 30, ItemType.Resource, GradeType.COMMON, 0, 0, "A piece of chicken."));
        items.Add(CreateItem("FoodPizza", 40, ItemType.Resource, GradeType.COMMON, 0, 0, "A slice of pizza."));
        items.Add(CreateItem("FoodShell", 50, ItemType.Resource, GradeType.COMMON, 0, 0, "A seafood shell."));
        items.Add(CreateItem("Hammer", 150, ItemType.Weapon, GradeType.COMMON, 0, 3, "A basic hammer."));
        items.Add(CreateItem("HorseShoe", 70, ItemType.Resource, GradeType.COMMON, 0, 0, "A lucky horseshoe."));
        items.Add(CreateItem("Magnet", 80, ItemType.Resource, GradeType.COMMON, 0, 0, "A strong magnet."));
        items.Add(CreateItem("Mail", 100, ItemType.Resource, GradeType.COMMON, 0, 0, "A mail envelope."));
        items.Add(CreateItem("Missile", 400, ItemType.Weapon, GradeType.EPIC, 0, 15, "A powerful missile."));
        items.Add(CreateItem("Nut", 30, ItemType.Resource, GradeType.COMMON, 0, 0, "A basic nut."));
        items.Add(CreateItem("Oil", 100, ItemType.Resource, GradeType.COMMON, 0, 0, "A bottle of oil."));
        items.Add(CreateItem("Potion_Blue", 150, ItemType.Potion, GradeType.COMMON, 0, 0, "A blue potion."));
        items.Add(CreateItem("Potion_Purple", 150, ItemType.Potion, GradeType.COMMON, 0, 0, "A purple potion."));
        items.Add(CreateItem("Potion_Red", 150, ItemType.Potion, GradeType.COMMON, 0, 0, "A red potion."));
        items.Add(CreateItem("BigPotion_Green", 150, ItemType.Potion, GradeType.COMMON, 0, 0, "A green potion."));
        items.Add(CreateItem("BigPotion_Purple", 150, ItemType.Potion, GradeType.COMMON, 0, 0, "A purple potion."));
        items.Add(CreateItem("BigPotion_Yellow", 150, ItemType.Potion, GradeType.COMMON, 0, 0, "A yellow potion."));
        items.Add(CreateItem("Pumpkin", 200, ItemType.Resource, GradeType.COMMON, 0, 0, "A Halloween pumpkin."));
        items.Add(CreateItem("Shield", 400, ItemType.Shield, GradeType.RARE, 10, 0, "A sturdy shield."));
        items.Add(CreateItem("Shovel", 100, ItemType.Resource, GradeType.COMMON, 0, 0, "A tool for digging."));
        items.Add(CreateItem("Star", 50, ItemType.Resource, GradeType.COMMON, 0, 0, "A star emblem."));
        items.Add(CreateItem("Sword", 300, ItemType.Weapon, GradeType.RARE, 0, 7, "A sharp sword."));
        items.Add(CreateItem("Tooth", 50, ItemType.Resource, GradeType.COMMON, 0, 0, "A tooth."));
       
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

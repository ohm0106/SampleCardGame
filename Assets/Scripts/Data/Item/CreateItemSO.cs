using UnityEngine;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using System.Collections.Generic;
using System.IO;

public class CreateItemSO
{
    public static List<ItemSO> items = new List<ItemSO>
    {
         CreateItem("Bomb_Bomb", 300, ItemType.Weapon, GradeType.COMMON, new Stat[] { new Stat { statType = StatType.Critical, value = 10 },new Stat { statType = StatType.AttackDamage , value = 20} }, "Bomb icon."),
         CreateItem("Bomb_Dynamite", 800, ItemType.Weapon, GradeType.EPIC, new Stat[] { new Stat { statType = StatType.Critical, value = 10 } ,new Stat { statType = StatType.AttackDamage , value = 50}}, "Bomb icon."),
         CreateItem("Boots", 200, ItemType.Armor, GradeType.COMMON, new Stat[] { new Stat { statType = StatType.Defense, value = 3 } ,new Stat { statType = StatType.Critical, value = 1 }}, "A pair of sturdy boots."),
         CreateItem("BoxingGloves", 250, ItemType.Weapon, GradeType.COMMON, new Stat[] { new Stat { statType = StatType.Critical, value = 3 }  ,new Stat { statType = StatType.AttackDamage, value = 10 }}, "Boxing gloves for combat."),
         CreateItem("Clover", 150, ItemType.Accessory, GradeType.EPIC, new Stat[] { new Stat { statType = StatType.Critical, value = 10 } }, "A clover that brings good luck."),
         CreateItem("Crown", 500, ItemType.Accessory, GradeType.LEGENDARY, new Stat[] { new Stat { statType = StatType.Defense, value = 10 }, new Stat { statType = StatType.Critical , value = 10} }, "A crown fit for a king."),
         CreateItem("DogGum", 30, ItemType.Resource, GradeType.COMMON, new Stat[] { }, "A bone for dogs."),
         CreateItem("Egg", 10, ItemType.Resource, GradeType.COMMON, new Stat[] { }, "A simple egg."),
         CreateItem("Emergency_Bag", 100, ItemType.Accessory, GradeType.COMMON, new Stat[] {new Stat { statType = StatType.Health, value = 10 } }, "Emergency supplies."),
         CreateItem("Flippers", 120, ItemType.Armor, GradeType.RARE, new Stat[] { new Stat { statType = StatType.Health, value = 10 }, new Stat {statType =StatType.Defense, value = 5 } }, "Flippers for swimming."),  
         CreateItem("Food_Can", 20, ItemType.Resource, GradeType.COMMON, new Stat[] { }, "A can of food."),
         CreateItem("Food_Pizza", 40, ItemType.Resource, GradeType.COMMON, new Stat[] { }, "A slice of pizza."),
         CreateItem("Food_Shell", 50, ItemType.Resource, GradeType.COMMON, new Stat[] { }, "A seafood shell."),
         CreateItem("Hammer", 150, ItemType.Weapon, GradeType.RARE, new Stat[] { new Stat { statType = StatType.Critical, value = 3 } , new Stat { statType = StatType.AttackDamage , value = 100}}, "A basic hammer."),
         CreateItem("Horsesheos", 70, ItemType.Accessory, GradeType.COMMON, new Stat[] { new Stat { statType = StatType.Critical, value = 3 } }, "A lucky horseshoe."),
         CreateItem("Magnetic", 80, ItemType.Resource, GradeType.COMMON, new Stat[] { }, "A strong magnet."),
         CreateItem("Missile", 400, ItemType.Weapon, GradeType.EPIC, new Stat[] { new Stat { statType = StatType.AttackDamage, value = 50 } }, "A powerful missile."),
         CreateItem("Nut", 30, ItemType.Resource, GradeType.COMMON, new Stat[] { }, "A basic nut."),
         CreateItem("Oil", 100, ItemType.Resource, GradeType.COMMON, new Stat[] { }, "A bottle of oil."),
         CreateItem("Potion_Blue", 150, ItemType.Potion, GradeType.COMMON, new Stat[] { }, "A blue potion."),
         CreateItem("Potion_Purple", 150, ItemType.Potion, GradeType.COMMON, new Stat[] { }, "A purple potion."),
         CreateItem("Potion_Red", 150, ItemType.Potion, GradeType.COMMON, new Stat[] { }, "A red potion."),
         CreateItem("BigPotion_Green", 150, ItemType.Potion, GradeType.COMMON, new Stat[] { }, "A green potion."),
         CreateItem("BigPotion_Purple", 150, ItemType.Potion, GradeType.COMMON, new Stat[] { }, "A purple potion."),
         CreateItem("BigPotion_Red", 150, ItemType.Potion, GradeType.COMMON, new Stat[] { }, "A yellow potion."),
         CreateItem("Pumkin", 200, ItemType.Armor, GradeType.RARE, new Stat[] { new Stat { statType = StatType.Defense, value = 20 },new Stat { statType = StatType.Health, value = 10 } }, "A Halloween pumpkin."),
         CreateItem("Shield", 400, ItemType.Shield, GradeType.COMMON, new Stat[] { new Stat { statType = StatType.Defense, value = 20 } ,new Stat { statType = StatType.Health, value = 5 }}, "A sturdy shield."),
         CreateItem("Shovel", 100, ItemType.Weapon, GradeType.RARE, new Stat[] { new Stat { statType = StatType.Critical, value = 5 } , new Stat { statType = StatType.AttackDamage , value = 30}}, "A tool for digging."),
         CreateItem("Sword", 300, ItemType.Weapon, GradeType.COMMON, new Stat[] { new Stat { statType = StatType.Critical, value = 3 } , new Stat { statType = StatType.AttackDamage , value = 10} }, "A sharp sword."),
         CreateItem("Tooth", 50, ItemType.Resource, GradeType.COMMON, new Stat[] { }, "A tooth.")
    };

    [MenuItem("Assets/Item/Create ItemSO Data")]
    public static void CreateItems()
    {
        foreach (var item in items)
        {
            string assetPath = $"Assets/SO/Items/{item.itemName}.asset";
            AssetDatabase.CreateAsset(item, assetPath);

            AddAddressableAsset(item, assetPath, "ItemGroup", new List<string> { "ItemGroup" });
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

    private static ItemSO CreateItem(string name, int price, ItemType itemType, GradeType gradeType, Stat[] stats, string description)
    {
        ItemSO newItem = ScriptableObject.CreateInstance<ItemSO>();
        newItem.itemName = name;
        newItem.price = price;
        newItem.itemType = itemType;
        newItem.gradeType = gradeType;
        newItem.stats = stats;
        newItem.icon = Resources.Load<Sprite>($"Sprite/ItemIcons/Icon_{name}");
        newItem.description = description;
        return newItem;
    }

    private static void AddAddressableAsset(ScriptableObject asset, string assetPath, string groupName, List<string> labels)
    {
        AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;

        AddressableAssetGroup group = settings.FindGroup(groupName);
        if (group == null)
        {
            group = settings.CreateGroup(groupName, false, false, false, settings.DefaultGroup.Schemas);
        }

        AddressableAssetEntry entry = settings.CreateOrMoveEntry(AssetDatabase.AssetPathToGUID(assetPath), group);
        entry.address = asset.name;

        foreach (var label in labels)
        {
            if (!settings.GetLabels().Contains(label))
            {
                settings.AddLabel(label);
            }
            entry.SetLabel(label, true);
        }

        settings.SetDirty(AddressableAssetSettings.ModificationEvent.EntryMoved, entry, true);
        AssetDatabase.SaveAssets();
    }
}

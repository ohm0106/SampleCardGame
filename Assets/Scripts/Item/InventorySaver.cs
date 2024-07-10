using UnityEngine;
using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

public static class InventorySaver 
{
    private static readonly string inventoryFilePath = Application.persistentDataPath + "/inventory.json";

    public static void ResetInventory()
    {
        InventoryData emptyInventory = new InventoryData
        {
            Items = new List<Item>()
        };

        SaveInventory(emptyInventory);
    }


    public static void SaveInventory(InventoryData data)
    {
        try
        {
            string jsonData = JsonConvert.SerializeObject(data);
            File.WriteAllText(inventoryFilePath, jsonData);
            Debugger.PrintLog("Inventory saved successfully.");
        }
        catch (Exception ex)
        {
            Debugger.PrintLog("Error saving inventory: " + ex.Message, LogType.Error);
        }
    }

    public static InventoryData LoadInventory()
    {
        try
        {
            if (!File.Exists(inventoryFilePath))
            {
                Debugger.PrintLog("Inventory file not found.");
                return null;
            }

            string jsonData = File.ReadAllText(inventoryFilePath);
            InventoryData data = JsonConvert.DeserializeObject<InventoryData>(jsonData);
            Debugger.PrintLog("Inventory loaded successfully.");
            return data;
        }
        catch (Exception ex)
        {
            Debugger.PrintLog("Error loading inventory: " + ex.Message, LogType.Error);
            return null;
        }
    }
}

[Serializable]
public class InventoryData
{
    public List<Item> Items { get; set; }
}

[Serializable]
public class Item
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public GradeType GradeType { get; set; }  
}

using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    InventoryData curInventoryData;

    public event Action<Item> onRemove;
    public event Action<Item> onAdd;

    private void Start()
    {
        curInventoryData = SaveLoadUtility.LoadData<InventoryData>(SaveLoadUtility.inventoryFilePath);

        if (curInventoryData.Items == null)
        {
            curInventoryData.Items = new List<Item>();
        }

        if (curInventoryData.Currency == null)
        {
            curInventoryData.Currency = new CurrencyData();
        }
    }

    private void OnDestroy()
    {
        SaveLoadUtility.SaveData<InventoryData>(curInventoryData, SaveLoadUtility.inventoryFilePath);
    }

    #region 아이템
    public void RemoveItem(Item newItem)
    {
        Item existingItem = curInventoryData.Items.Find(item => item.Name == newItem.Name);
        if (existingItem != null)
        {
            existingItem.Quantity -= newItem.Quantity;
            if (existingItem.Quantity <= 0)
            {
                curInventoryData.Items.Remove(existingItem);
            }
        }

        onRemove?.Invoke(newItem);
    }

    public void AddItem(Item newItem)
    {
        Item existingItem = null;

        if (curInventoryData.Items.Count != 0)
        {
            existingItem = curInventoryData.Items.Find(item => item.Name == newItem.Name);
        }

        if (existingItem != null)
        {
            existingItem.Quantity += newItem.Quantity;
        }
        else
        {
            curInventoryData.Items.Add(newItem);
            // todo 리스트 추가 
        }

        onAdd?.Invoke(newItem);
    }

    public List<Item> GetItemsByTypes(List<ItemType> itemTypes)
    {
        return curInventoryData.Items.FindAll(item => itemTypes.Contains(ItemLibrary.Instance.GetItem(item.Name).itemType));
    }

    public List<Item> GetCurItemsData()
    {
        return curInventoryData.Items;
    }

    public InventoryData GetCurInventoryData()
    {
        return curInventoryData; 
    }
    #endregion

    #region 재화 
    public void AddCoins(int amount)
    {
        curInventoryData.Currency.Coins += amount;
        SaveLoadUtility.SaveData<InventoryData>(curInventoryData, SaveLoadUtility.inventoryFilePath);
    }

    public bool RemoveCoins(int amount)
    {
        if (curInventoryData.Currency.Coins >= amount)
        {
            curInventoryData.Currency.Coins -= amount;
            SaveLoadUtility.SaveData<InventoryData>(curInventoryData, SaveLoadUtility.inventoryFilePath);
            return true;
        }
        return false;
    }

    public void AddGems(int amount)
    {
        curInventoryData.Currency.Gems += amount;
        SaveLoadUtility.SaveData<InventoryData>(curInventoryData, SaveLoadUtility.inventoryFilePath);
    }

    public bool RemoveGems(int amount)
    {
        if (curInventoryData.Currency.Gems >= amount)
        {
            curInventoryData.Currency.Gems -= amount;
            SaveLoadUtility.SaveData<InventoryData>(curInventoryData, SaveLoadUtility.inventoryFilePath);
            return true;
        }
        return false;
    }

    public void AddEnergy(int amount)
    {
        curInventoryData.Currency.Energy += amount;
        SaveLoadUtility.SaveData<InventoryData>(curInventoryData, SaveLoadUtility.inventoryFilePath);
    }

    public bool RemoveEnergy(int amount)
    {
        if (curInventoryData.Currency.Energy >= amount)
        {
            curInventoryData.Currency.Energy -= amount;
            SaveLoadUtility.SaveData<InventoryData>(curInventoryData, SaveLoadUtility.inventoryFilePath);
            return true;
        }
        return false;
    }

    #endregion
}

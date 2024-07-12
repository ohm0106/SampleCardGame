using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    InventoryData curInventoryData;

    public event Action<Item> onRemoveItem;
    public event Action<Item> onAddItem;

    public event Action<int> onAddCoin;
    public event Action<int> onAddGem;
    public event Action<int> onAddEnergy;

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

        onRemoveItem?.Invoke(newItem);
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

        onAddItem?.Invoke(newItem);
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
    public bool AddCoins(int amount)
    {
        if ((curInventoryData.Currency.Coins + amount) > 0)
        {
            curInventoryData.Currency.Coins += amount;
            onAddCoin?.Invoke(curInventoryData.Currency.Coins);
        }
        else
        {
            return false;
        }
        return true;
    }

    public bool AddGems(int amount)
    {
        if ((curInventoryData.Currency.Gems + amount) > 0)
        {
            curInventoryData.Currency.Gems += amount;
            onAddGem?.Invoke(curInventoryData.Currency.Gems);
        }
        else
        {
            return false;
        }
        return true;

    }


    public bool AddEnergy(int amount)
    {
        if ((curInventoryData.Currency.Energy + amount) > 0)
        {
            curInventoryData.Currency.Energy += amount;
            onAddEnergy?.Invoke(curInventoryData.Currency.Energy);
        }
        else
        {
            return false;
        }
        return true;
    }


    #endregion
}

using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    InventoryData curInventoryData;

    public event Action<Item> onRemove;
    public event Action<Item> onAdd;

    private void OnEnable()
    {
        curInventoryData = SaveLoadUtility.LoadData<InventoryData>(SaveLoadUtility.inventoryFilePath);
    }

    private void OnDisable()
    {
        SaveLoadUtility.SaveData<InventoryData>(curInventoryData,SaveLoadUtility.inventoryFilePath);
    }

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

    public InventoryData GetCurInventoryData()
    {
        return curInventoryData;
    }

}

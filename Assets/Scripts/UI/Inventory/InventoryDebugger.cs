using System.Collections.Generic;
using UnityEngine;

public class InventoryDebugger : MonoBehaviour
{
    [SerializeField]
    private Inventory inventory;

    public int selectedItemIndex = 0; 
    public int newItemQuantity = 1;

    private void OnValidate()
    {
        if (inventory == null)
        {
            inventory = FindObjectOfType<Inventory>();
        }
    }

    public void AddSelectedItem()
    {
        if (selectedItemIndex >= 0 && selectedItemIndex < CreateItemSO.items.Count && newItemQuantity > 0)
        {
            Item item = new Item();
            item.Name = CreateItemSO.items[selectedItemIndex].itemName;
            item.Quantity = newItemQuantity;
            inventory.AddItem(item);
        }
    }

    public void RemoveSelectedItem()
    {
        if (selectedItemIndex >= 0 && selectedItemIndex < CreateItemSO.items.Count)
        {
            
            Item itemToRemove = new Item
            {
                Name = CreateItemSO.items[selectedItemIndex].itemName,
                Quantity = newItemQuantity
            };
            inventory.RemoveItem(itemToRemove);
        }
    }

    public void SaveInventory()
    {
        InventorySaver.SaveInventory(inventory.GetCurInventoryData());
    }

    public void LoadInventory()
    {
        InventoryData data = InventorySaver.LoadInventory();
        if (data != null)
        {
            inventory.GetCurInventoryData().Items = data.Items;
        }
    }

    public void ResetInventroy()
    {
        InventorySaver.ResetInventory();
        inventory.GetCurInventoryData().Items = new List<Item>();
    }
}

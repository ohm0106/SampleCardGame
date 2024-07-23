using System.Collections.Generic;
using UnityEngine;

public class InventoryList : BaseUIList<Item, ItemSlot>
{
    protected override void SubscribeToEvents()
    {
        SingletonManager.Instance.Inventory.onAddItem += UpdateInventoryUI;
        SingletonManager.Instance.Inventory.onRemoveItem += DeleteInventoryUI;
    }

    protected override void UnsubscribeFromEvents()
    {
        SingletonManager.Instance.Inventory.onAddItem -= UpdateInventoryUI;
        SingletonManager.Instance.Inventory.onRemoveItem -= DeleteInventoryUI;
    }

    protected override List<Item> GetItems()
    {
        return SingletonManager.Instance.Inventory.GetCurItemsData();
    }

    protected override void UpdateSlot(ItemSlot slot, Item item)
    {
        slot.UpdateSlot(item);
    }

    protected override void ClearSlot(ItemSlot slot)
    {
        slot.ClearSlot();
    }

    protected override bool IsSlotFilled(ItemSlot slot)
    {
        return slot.GetItem() != null;
    }

    protected override int CompareSlots(ItemSlot a, ItemSlot b)
    {
        return b.GetItem().GradeType.CompareTo(a.GetItem().GradeType);
    }

    public void UpdateInventoryUI(Item newItem)
    {
        foreach (var slot in slots)
        {
            if (slot.GetItem() != null && slot.GetItem().Name == newItem.Name)
            {
                newItem.Quantity += slot.GetItem().Quantity;
                slot.UpdateSlot(newItem);
                ReorganizeSlots();
                return;
            }
        }

        foreach (var slot in slots)
        {
            if (slot.GetItem() == null)
            {
                slot.UpdateSlot(newItem);
                ReorganizeSlots();
                return;
            }
        }
    }

    public void DeleteInventoryUI(Item newItem)
    {
        foreach (var slot in slots)
        {
            if (slot.GetItem() != null && slot.GetItem().Name == newItem.Name)
            {
                slot.GetItem().Quantity -= newItem.Quantity;
                if (slot.GetItem().Quantity <= 0)
                {
                    slot.ClearSlot();
                }
                else
                {
                    slot.UpdateSlot(slot.GetItem());
                }
                ReorganizeSlots();
                break;
            }
        }
    }

    public void FilterItemsByType(List<ItemType> itemTypes)
    {
        ClearAllSlots();

        var itemsByType = SingletonManager.Instance.Inventory.GetItemsByTypes(itemTypes);

        Debug.Log("check item By Type " + itemsByType.Count);

        for (int i = 0; i < itemsByType.Count && i < listSize; i++)
        {
            Item tempItem = itemsByType[i];
            slots[i].UpdateSlot(tempItem);
        }

        ReorganizeSlots();
    }
}



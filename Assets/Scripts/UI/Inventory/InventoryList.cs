using System.Collections.Generic;
using UnityEngine;

public class InventoryList : MonoBehaviour
{
    [SerializeField]
    int inventorySize = 50;

    [SerializeField]
    Transform scrollRect;

    [SerializeField]
    GameObject slotPrefab;

    Slot[] slots;
    
    void OnEnable()
    {
        InitializeSlots();
        SingletonManager.Instance.Inventory.onAddItem += UpdateInventoryUI;
        SingletonManager.Instance.Inventory.onRemoveItem += DeleteInventoryUI;
    }

    void OnDisable()
    {
        ClearAllSlots();
        SingletonManager.Instance.Inventory.onAddItem -= UpdateInventoryUI;
        SingletonManager.Instance.Inventory.onRemoveItem -= DeleteInventoryUI;
    }

    void InitializeSlots()
    {
        slots = new Slot[inventorySize];

        var itemsInfo = SingletonManager.Instance.Inventory.GetCurItemsData();

        for (int i = 0; i < inventorySize; i++)
        {
            GameObject slotObj = Instantiate(slotPrefab, scrollRect);
            slots[i] = slotObj.GetComponent<Slot>();

            if (i < itemsInfo.Count)
            {
                Item tempItem = itemsInfo[i];
                slots[i].UpdateSlot(tempItem);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }

        ReorganizeSlots();
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

        for (int i = 0; i < itemsByType.Count && i < inventorySize; i++)
        {
            Item tempItem = itemsByType[i];
            slots[i].UpdateSlot(tempItem);
        }

        ReorganizeSlots();
    }

    void ReorganizeSlots()
    {
        List<Slot> filledSlots = new List<Slot>();
        List<Slot> emptySlots = new List<Slot>();

        foreach (var slot in slots)
        {
            if (slot.GetItem() != null)
            {
                filledSlots.Add(slot);
            }
            else
            {
                emptySlots.Add(slot);
            }
        }

        filledSlots.Sort((a, b) => b.GetItem().GradeType.CompareTo(a.GetItem().GradeType));

        int index = 0;
        foreach (var slot in filledSlots)
        {
            slots[index] = slot;
            slots[index].transform.SetSiblingIndex(index);
            index++;
        }

        foreach (var slot in emptySlots)
        {
            slots[index] = slot;
            slots[index].transform.SetSiblingIndex(index);
            index++;
        }
    }

    void ClearAllSlots()
    {
        foreach (var slot in slots)
        {
            slot.ClearSlot();
        }
    }

}

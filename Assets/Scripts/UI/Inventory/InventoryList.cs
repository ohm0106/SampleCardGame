using System.Collections.Generic;
using UnityEngine;

public class InventoryList : MonoBehaviour
{
    [SerializeField]
    int inventroySize = 50;

    [SerializeField]
    Transform scrollRect;

    [SerializeField]
    GameObject slotPrefab;

    Slot[] slots;
    List<Slot> emptySlots = new List<Slot>();
    List<Slot> filledSlots = new List<Slot>();

    void OnEnable()
    {
        InitializeSlots();
        SingletonManager.Instance.Inventory.onAdd += UpdateInventoryUI;
        SingletonManager.Instance.Inventory.onRemove += DleteInventoryUI;
    }

    void OnDisable()
    {
        ClearAllSlots();
        SingletonManager.Instance.Inventory.onAdd -= UpdateInventoryUI;
        SingletonManager.Instance.Inventory.onRemove -= DleteInventoryUI;
    }

    void InitializeSlots()
    {
        slots = new Slot[inventroySize];

        var inven_info = SingletonManager.Instance.Inventory.GetCurInventoryData();

        for (int i = 0; i < inventroySize; i++)
        {
            GameObject slotObj = Instantiate(slotPrefab, scrollRect);
            slots[i] = slotObj.GetComponent<Slot>();

            if (i < inven_info.Items.Count)
            {
                Item tempItem = inven_info.Items[i];
                slots[i].UpdateSlot(tempItem);
                filledSlots.Add(slots[i]);
            }
            else
            {
                emptySlots.Add(slots[i]);
            }
        }
    }
    public void UpdateInventoryUI(Item newItem)
    {
        foreach (var slot in filledSlots)
        {
            if ( !slot.CheckEmpty() && slot.GetItem().Name == newItem.Name)
            {
                slot.UpdateSlot(newItem);
                return;
            }
        }

        if (emptySlots.Count > 0)
        {
            Slot slot = emptySlots[0];
            slot.UpdateSlot(newItem);
            filledSlots.Add(slot);
            emptySlots.RemoveAt(0);
        }


    }
    public void DleteInventoryUI(Item newItem)
    {
        foreach (var slot in filledSlots)
        {
            if (slot.GetItem() != null && slot.GetItem().Name == newItem.Name)
            {
                slot.GetItem().Quantity -= newItem.Quantity;
                if (slot.GetItem().Quantity <= 0)
                {
                    slot.ClearSlot();
                    emptySlots.Add(slot);
                }
                break;
            }
        }

        ReorganizeSlots();
    }

    void ReorganizeSlots()
    {
        filledSlots.RemoveAll(slot => slot.GetItem() == null);
        filledSlots.Sort((a, b) => a.transform.GetSiblingIndex().CompareTo(b.transform.GetSiblingIndex()));

        foreach (var slot in filledSlots)
        {
            slot.transform.SetAsLastSibling();
        }

        emptySlots.Clear();
        foreach (var slot in slots)
        {
            if (slot.GetItem() == null)
            {
                emptySlots.Add(slot);
            }
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

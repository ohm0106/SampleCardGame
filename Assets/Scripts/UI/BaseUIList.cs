using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUIList<T, TSlot> : MonoBehaviour where TSlot : MonoBehaviour
{
    [SerializeField]
    protected int listSize = 50;

    [SerializeField]
    Transform scrollContent;

    [SerializeField]
    GameObject slotPrefab;

    protected TSlot[] slots;

    protected virtual void OnEnable()
    {
        InitializeSlots();
        SubscribeToEvents();
    }

    protected virtual void OnDisable()
    {
        ClearAllSlots();
        UnsubscribeFromEvents();
    }

    void InitializeSlots()
    {
        if (slots != null)
            return;

        slots = new TSlot[listSize];

        var itemsInfo = GetItems();

        for (int i = 0; i < listSize; i++)
        {
            GameObject slotObj = Instantiate(slotPrefab, scrollContent);
            slots[i] = slotObj.GetComponent<TSlot>();

            if (i < itemsInfo.Count)
            {
                T temp = itemsInfo[i];
                UpdateSlot(slots[i], temp);
            }
            else
            {
                ClearSlot(slots[i]);
            }
        }

        ReorganizeSlots();
    }

    protected abstract List<T> GetItems();
    protected abstract void UpdateSlot(TSlot slot, T item);
    protected abstract void ClearSlot(TSlot slot);
    protected abstract void SubscribeToEvents();
    protected abstract void UnsubscribeFromEvents();

    protected void ClearAllSlots()
    {
        foreach (var slot in slots)
        {
            ClearSlot(slot);
        }
    }

    protected void ReorganizeSlots()
    {
        List<TSlot> filledSlots = new List<TSlot>();
        List<TSlot> emptySlots = new List<TSlot>();

        foreach (var slot in slots)
        {
            if (IsSlotFilled(slot))
            {
                filledSlots.Add(slot);
            }
            else
            {
                emptySlots.Add(slot);
            }
        }

        filledSlots.Sort((a, b) => CompareSlots(a, b));

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

    protected abstract bool IsSlotFilled(TSlot slot);
    protected abstract int CompareSlots(TSlot a, TSlot b);
}

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

    // todo : 탭 (아이템 타입) 별 slot 관리 
    // todo : GradeType 에 맞게 리스트 정렬 

    void OnEnable()
    {
        InitializeSlots();
        SingletonManager.Instance.Inventory.onAdd += UpdateInventoryUI;
        SingletonManager.Instance.Inventory.onRemove += DeleteInventoryUI;
    }

    void OnDisable()
    {
        ClearAllSlots();
        SingletonManager.Instance.Inventory.onAdd -= UpdateInventoryUI;
        SingletonManager.Instance.Inventory.onRemove -= DeleteInventoryUI;
    }

    void InitializeSlots()
    {
        slots = new Slot[inventorySize];

        var inven_info = SingletonManager.Instance.Inventory.GetCurInventoryData();

        for (int i = 0; i < inventorySize; i++)
        {
            GameObject slotObj = Instantiate(slotPrefab, scrollRect);
            slots[i] = slotObj.GetComponent<Slot>();

            if (i < inven_info.Items.Count)
            {
                Item tempItem = inven_info.Items[i];
                slots[i].UpdateSlot(tempItem);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }

    public void UpdateInventoryUI(Item newItem)
    {
        foreach (var slot in slots)
        {
            if (slot.GetItem() != null && slot.GetItem().Name == newItem.Name)
            {
                slot.GetItem().Quantity += newItem.Quantity;
                slot.UpdateSlot(slot.GetItem());
                return;
            }
        }

        foreach (var slot in slots)
        {
            if (slot.GetItem() == null)
            {
                slot.UpdateSlot(newItem);
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
                break;
            }
        }

        ReorganizeSlots();
    }

    void ReorganizeSlots()
    {
        List<Slot> tempSlots = new List<Slot>();

        // 채워진 슬롯을 tempSlots에 추가
        foreach (var slot in slots)
        {
            if (slot.GetItem() != null)
            {
                tempSlots.Add(slot);
            }
        }

        // 빈 슬롯을 tempSlots에 추가
        foreach (var slot in slots)
        {
            if (slot.GetItem() == null)
            {
                tempSlots.Add(slot);
            }
        }

        // tempSlots의 순서대로 slots 배열 업데이트 및 인덱스 재설정
        for (int i = 0; i < tempSlots.Count; i++)
        {
            slots[i] = tempSlots[i];
            slots[i].transform.SetSiblingIndex(i);
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

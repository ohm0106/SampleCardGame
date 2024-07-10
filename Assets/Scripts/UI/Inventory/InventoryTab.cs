using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class InventoryTab : MonoBehaviour
{
    public ItemTypeMask itemTypeMask;
    private Button btn;

    private void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(ClickEvent);
    }

    public List<ItemType> FilterItemsBySelectedTypes()
    {
        List<ItemType> selectedTypes = new List<ItemType>();

        Debug.Log("itemTypeMask.mask: " + itemTypeMask.mask);

        foreach (ItemType type in Enum.GetValues(typeof(ItemType)))
        {
            if (type == ItemType.None)
                continue;

            if ((itemTypeMask.mask & (int)type) == (int)type)
            {
                selectedTypes.Add(type);
            }
        }

        Debug.Log("Filtered ItemTypes: " + string.Join(", ", selectedTypes));
        return selectedTypes;
    }

    void ClickEvent()
    {
        InventoryList list = FindObjectOfType<InventoryList>();
        if (list != null)
        {
            List<ItemType> selectedTypes = FilterItemsBySelectedTypes();
            list.FilterItemsByType(selectedTypes);
        }
        else
        {
            Debug.LogError("InventoryList not found.");
        }
    }
}

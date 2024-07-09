using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Slot : MonoBehaviour
{

    [SerializeField]
    Image Slot_item;

    [SerializeField]
    Image ItemIcon;

    [SerializeField]
    TMP_Text itemQuantity;

    Item item;

    public void UpdateSlot(Item curItem)
    {
        Slot_item.gameObject.SetActive(true); 
        //Slot_item.sprite = slotImage; // todo 
        Sprite itemIcon = ItemLibrary.Instance.GetItem(curItem.Name).icon;
        ItemIcon.sprite = itemIcon;
        if (curItem.Quantity > 1)
            itemQuantity.text = $"{curItem.Quantity}";
        else
            itemQuantity.text = "";
        item = curItem;
    }

    public void ClearSlot()
    {
        ItemIcon.sprite = null;
        itemQuantity.text = "";
        Slot_item.gameObject.SetActive(false);
        item = null;
    }

    public bool CheckEmpty()
    {
        if (item == null)
            return true;
        else
            return false;
    }

    public Item GetItem()
    {
        return item;
    }
}

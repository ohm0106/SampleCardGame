using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : BaseSlot
{
    [SerializeField]
    Image itemIcon;
    [SerializeField]
    TMP_Text itemQuantity;

    Item item;

    public override void UpdateSlot(object data)
    {
        Item curItem = data as Item;
        if (curItem == null)
        {
            Debug.LogError("Invalid item data");
            return;
        }

        slotImg.gameObject.SetActive(true);
        Sprite itemIconSprite = ResourceLibrary.Instance.ItemLibrary.GetItem(curItem.Name).icon;
        slotImg.sprite = ResourceLibrary.Instance.ItemLibrary.GetSlotImg(ResourceLibrary.Instance.ItemLibrary.GetItem(curItem.Name).gradeType);
        itemIcon.sprite = itemIconSprite;

        if (curItem.Quantity > 1)
            itemQuantity.text = $"{curItem.Quantity}";
        else
            itemQuantity.text = "";

        item = curItem;
    }

    public override void ClearSlot()
    {
        itemIcon.sprite = null;
        itemQuantity.text = "";
        slotImg.gameObject.SetActive(false);
        item = null;
    }

    public void ClickBtn()
    {
        if (item == null)
            return;

        Debug.Log("Item: " + item.Name);
        FindObjectOfType<ItemInfomation>().SetUI(item);
    }

    public bool CheckEmpty()
    {
        return item == null;
    }

    public Item GetItem()
    {
        return item;
    }
}

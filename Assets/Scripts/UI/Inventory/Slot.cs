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
        Sprite itemIcon = ResourceLibrary.Instance.ItemLibrary.GetItem(curItem.Name).icon;
        Slot_item.sprite = ResourceLibrary.Instance.ItemLibrary.GetSlotImg(ResourceLibrary.Instance.ItemLibrary.GetItem(curItem.Name).gradeType); // todo 
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

    public void ClickBtn()
    {
        if (item == null)
            return;

        Debug.Log("item" + item.Name);

        FindObjectOfType<ItemInfomation>().SetUI(item);
        ObjectClickHandler.InvokeClick(GetComponent<RectTransform>());
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

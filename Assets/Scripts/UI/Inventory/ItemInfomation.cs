using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemInfomation : MonoBehaviour
{
    [SerializeField]
    GameObject item_infomation_obj;

    [SerializeField]
    TMP_Text grade;
    [SerializeField]
    TMP_Text name;
    [SerializeField]
    TMP_Text info;
    [SerializeField]
    TMP_Text attackDamage;
    [SerializeField]
    TMP_Text defenseDamage;
    [SerializeField]
    TMP_Text price;
    [SerializeField]
    Slot slot;

    public void SetUI(Item item)
    {
        ItemSO tempItemSO = ItemLibrary.Instance.GetItem(item.Name);
        
        if (tempItemSO == null)
        {
            Debugger.PrintLog("Doesnt Exist Item " + item.Name, LogType.Error);
            return;
        }
        
        item_infomation_obj.SetActive(true);

        grade.text = tempItemSO.gradeType.ToString();
        name.text = tempItemSO.itemName;
        info.text = tempItemSO.description;
        attackDamage.text = "+" + tempItemSO.critical;
        defenseDamage.text = "+" + tempItemSO.defense;
        price.text = tempItemSO.price.ToString();
        slot.UpdateSlot(item);
    }

    public void Release()
    {
        item_infomation_obj.SetActive(false);
        grade.text = "";
        name.text = "";
        info.text = "";
        attackDamage.text = "";
        defenseDamage.text = "";
        price.text = "";
    }

}

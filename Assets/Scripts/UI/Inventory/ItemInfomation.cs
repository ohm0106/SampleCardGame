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
    TMP_Text stat_text1;
    [SerializeField]
    TMP_Text stat_text2;
    [SerializeField]
    TMP_Text price;
    [SerializeField]
    Slot slot;

    private void OnDisable()
    {
        Release();
    }

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
        // 각 등록된 stat 에 따라 변경할 것. 
        //attackDamage.text = "+" + tempItemSO.critical;
        //defenseDamage.text = "+" + tempItemSO.defense;
        price.text = tempItemSO.price.ToString();
        slot.UpdateSlot(item);
    }

    public void Release()
    {
        if (slot.GetItem() == null)
            return;

        item_infomation_obj.SetActive(false);
        grade.text = "";
        name.text = "";
        info.text = "";
        //attackDamage.text = "";
        //defenseDamage.text = "";
        price.text = "";
        slot.ClearSlot();
    }

}

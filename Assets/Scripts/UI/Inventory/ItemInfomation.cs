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
    TMP_Text price;
    [SerializeField]
    ItemSlot slot;

    [SerializeField]
    RectTransform statParent;

    private void OnDisable()
    {
        Release();
    }

    public void SetUI(Item item)
    {
        ItemSO tempItemSO = ResourceLibrary.Instance.ItemLibrary.GetItem(item.Name);

        if (tempItemSO == null)
        {
            Debugger.PrintLog("Doesnt Exist Item " + item.Name, LogType.Error);
            return;
        }

        item_infomation_obj.SetActive(true);

        grade.text = tempItemSO.gradeType.ToString();
        name.text = tempItemSO.itemName;
        info.text = tempItemSO.description;

        var statBars = statParent.GetComponentsInChildren<StatBar>();

        for (int i = 0; i < tempItemSO.stats.Length; i++)
        {
            if(i < statBars.Length)
            {
                statBars[i].SetStat(ResourceLibrary.Instance.StatIconLibrary.GetStatIcon(tempItemSO.stats[i].statType).icon, tempItemSO.stats[i]);
            }
            else
            {
                StatBar created = Instantiate(statBars[0], statParent);
                created.SetStat(ResourceLibrary.Instance.StatIconLibrary.GetStatIcon(tempItemSO.stats[i].statType).icon, tempItemSO.stats[i]);
            }
        }
        
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

        var statBars = statParent.GetComponentsInChildren<StatBar>();

        foreach(var statBar in statBars)
        {
            statBar.Release();
        }

        price.text = "";
        slot.ClearSlot();
    }

}

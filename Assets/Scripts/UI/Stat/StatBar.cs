using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class StatBar : MonoBehaviour
{
    [SerializeField]
    StatType statType;

    [SerializeField]
    Image staticon;

    [SerializeField]
    TMP_Text statnameTxt;

    [SerializeField]
    TMP_Text statValueTxt;

    public void SetStat(Sprite icon, Stat stat)
    {
        statnameTxt.text = stat.statType.ToString();
        statType = stat.statType;
        staticon.sprite = icon;
        statValueTxt.text = $"{stat.value}";
    }

    public void Release()
    {
        statnameTxt.text = "";
        staticon.sprite = null;
        statValueTxt.text = "";
    }


}

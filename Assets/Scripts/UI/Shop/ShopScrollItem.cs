using UnityEngine;
using TMPro;
public class ShopScrollItem : MonoBehaviour
{
    [SerializeField]
    RectTransform tabRect;
    TMP_Text tabTxt;
    Color color;
    private void Start()
    {
        tabTxt = tabRect.GetComponent<TMP_Text>();
        color = tabTxt.color;
    }

    public RectTransform GetTabRect()
    {
        tabTxt.color = Color.white;
        return tabRect;
    }

    public void Release()
    {
        tabTxt.color = color;
    }
}

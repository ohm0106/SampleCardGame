using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectClickHandler : MonoBehaviour
{
    public static event Action<RectTransform> onClickSlot;

    public void OnEnable()
    {
        onClickSlot += ClickSlot;
    }
    private void OnDisable()
    {
        onClickSlot -= ClickSlot;
    }
    public static void InvokeClick(RectTransform r)
    {
        onClickSlot?.Invoke(r);
    }
    void ClickSlot(RectTransform t)
    {
        RectTransform slotRectTransform = GetComponent<RectTransform>();
        slotRectTransform.SetParent(t);
        slotRectTransform.sizeDelta = Vector2.zero;
        slotRectTransform.anchoredPosition = Vector2.zero;
        slotRectTransform.localScale = Vector3.one;
    }
}


public interface ISlot
{
    // 식별을 위한 인터페이스 생성
}
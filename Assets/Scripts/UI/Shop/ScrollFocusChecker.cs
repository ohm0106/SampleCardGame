using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using DG.Tweening;

public class ScrollFocusChecker : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    [SerializeField]
    RectTransform focusTab;

    [SerializeField]
    ScrollRect scrollRect;

    [SerializeField]
    RectTransform content;

    [SerializeField]
    List<RectTransform> items;

    bool isDragging;

    private void Start()
    {
        if (scrollRect == null)
        {
            scrollRect = GetComponent<ScrollRect>();
            if (scrollRect == null)
            {
                Debugger.PrintLog("ScrollRect component not found.");
                return;
            }
        }

        if (content == null)
        {
            content = scrollRect.content;
            if (content == null)
            {
                Debugger.PrintLog("Content not assigned or found in ScrollRect.",LogType.Error);
                return;
            }
        }

        InitializeItems();
    }

    private void InitializeItems()
    {
        if (items == null || items.Count == 0)
        {
            items = new List<RectTransform>();
            foreach (Transform child in content)
            {
                RectTransform rectTransform = child.GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    items.Add(rectTransform);
                }
            }
        }
    }

    private void Update()
    {
        if (!isDragging)
        {
            CheckFocus();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        CheckFocus();
    }

    RectTransform preClosestItem = null;

    private void CheckFocus()
    {
        RectTransform curClosestItem = null;
        float closestDistance = float.MaxValue;

        Vector3[] scrollRectCorners = new Vector3[4];
        scrollRect.GetComponent<RectTransform>().GetWorldCorners(scrollRectCorners);
        Vector3 scrollRectCenter = (scrollRectCorners[0] + scrollRectCorners[2]) / 2;

        foreach (var item in items)
        {
            float distance = Vector2.Distance(item.position, scrollRectCenter);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                curClosestItem = item;
            }
        }

        if (curClosestItem != preClosestItem)
        {
            foreach (var item in items)
            {
                ShopScrollItem temp = item.GetComponent<ShopScrollItem>();
                if (curClosestItem != item)
                {

                    temp.Release();
                }
                else
                {
                    focusTab.anchoredPosition = new Vector3(temp.GetTabRect().anchoredPosition.x, focusTab.anchoredPosition.y, 0);
                }
            }
            preClosestItem = curClosestItem;
        }
      
     
    }

}

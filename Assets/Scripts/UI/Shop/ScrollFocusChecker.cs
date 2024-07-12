using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using DG.Tweening;

public class ScrollFocusChecker : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    [SerializeField]
    private ScrollRect scrollRect;

    [SerializeField]
    private RectTransform content;

    [SerializeField]
    private List<RectTransform> items;

    private bool isDragging;

    private void Start()
    {
        if (scrollRect == null)
        {
            scrollRect = GetComponent<ScrollRect>();
            if (scrollRect == null)
            {
                Debug.LogError("ScrollRect component not found.");
                return;
            }
        }

        if (content == null)
        {
            content = scrollRect.content;
            if (content == null)
            {
                Debug.LogError("Content not assigned or found in ScrollRect.");
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

    private void CheckFocus()
    {
        RectTransform closestItem = null;
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
                closestItem = item;
            }
        }

        if (closestItem != null)
        {
            Debugger.PrintLog($"Item {closestItem.name} is in focus");
        }
    }

    public void ScrollToItem(int itemIndex)
    {
        if (itemIndex < 0 || itemIndex >= items.Count)
        {
            Debugger.PrintLog("Invalid item index",LogType.Warning);
            return;
        }

        RectTransform targetItem = items[itemIndex];
        Canvas.ForceUpdateCanvases();

        float contentHeight = content.rect.height;
        float viewportHeight = scrollRect.viewport.rect.height;
        float itemCenterY = targetItem.localPosition.y + (targetItem.rect.height / 2);

        float normalizedPositionY = (itemCenterY - (viewportHeight / 2)) / (contentHeight - viewportHeight);
        normalizedPositionY = Mathf.Clamp01(1 - normalizedPositionY);

        DOTween.To(() => scrollRect.verticalNormalizedPosition, x => scrollRect.verticalNormalizedPosition = x, normalizedPositionY, 0.5f).SetEase(Ease.InOutQuad);
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class BaseSlot : MonoBehaviour
{
    [SerializeField]
    protected Image slotImg;

    protected bool isEmpty;

    public abstract void UpdateSlot(object data);
    public abstract void ClearSlot();

    protected void SetActiveStateForChildren(bool state)
    {
        Transform[] children = GetComponentsInChildren<Transform>(true);
        foreach (var child in children)
        {
            if (child != this.transform)
            {
                child.gameObject.SetActive(state);
            }
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PopupItem : MonoBehaviour
{
    [SerializeField]
    TMP_Text title;
    [SerializeField]
    TMP_Text context;

    public Action Button1;
    public Action Button2;
    public Action Button3;
    public Action Close;
    public Action End;

    public void OnButton1()
    {
        Button1?.Invoke();
        End?.Invoke();
    }

    public void OnButton2()
    {
        Button2?.Invoke();
        End?.Invoke();
    }

    public void OnButton3()
    {
        Button3?.Invoke();
        End?.Invoke();
    }

    public void OnClose()
    {
        Close?.Invoke();
        End?.Invoke();
    }

    public void SetText(string title, string context)
    {
        if (this.title != null)
            this.title.text = title;

        if (this.context != null)
            this.context.text = context;
    }

}



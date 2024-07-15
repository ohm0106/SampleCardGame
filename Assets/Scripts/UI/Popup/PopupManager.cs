using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;

public enum PopupType
{
    Default,
    Big_Default,
    Agree,
    Ok,
    Setting,
    Update,
    UserInfo,
    EditText_Button
}

public class PopupManager : Singleton<PopupManager>
{
    GameObject createdPopup;
    bool isPopupOn = false;

    private Queue<PopupRequest> popupQueue = new Queue<PopupRequest>();

    int count = 0;

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyUp(KeyCode.P))
        {
            OpenCustomPopup(PopupType.Default,"test","This is Test" + count++);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            Open(PopupType.UserInfo);
        }
#endif
    }

    public void Open(PopupType type, Action onButton1 = null, Action onButton2 = null, Action onButton3 = null, Action onClose = null)
    {
        Debugger.PrintLog("IsPopup On = " + isPopupOn);
        if (isPopupOn)
        {
            // 현재 팝업이 열려있으면 큐에 추가
            popupQueue.Enqueue(new PopupRequest(type, onButton1, onButton2, onButton3, onClose));
            return;
        }

        isPopupOn = true;

        // load popup
        var popup = Resources.Load<GameObject>("Prefab/Popup/" + type.ToString());

        if (popup == null)
        {
            Debugger.PrintLog(type.ToString() + " popup is missing!");
            return;
        }

        GameObject parentObj = FindObjectOfType<Canvas>().gameObject;

        if (parentObj == null)
        {
            Debugger.PrintLog("Empty Canvas", LogType.Error);
            return;
        }

        createdPopup = Instantiate(popup, parentObj.transform);
        var popupItem = createdPopup.GetComponent<PopupItem>();

        popupItem.Button1 = onButton1;
        popupItem.Button2 = onButton2;
        popupItem.Button3 = onButton3;
        popupItem.Close = onClose;
        popupItem.End = () =>
        {
            isPopupOn = false;

            Debugger.PrintLog("Close Popup");
            if (createdPopup != null)
                Destroy(createdPopup);

            createdPopup = null;

            // 큐에서 다음 팝업 요청 처리
            if (popupQueue.Count > 0)
            {
                var nextPopup = popupQueue.Dequeue();
                OpenCustomPopup(nextPopup.Type, nextPopup.Title, nextPopup.Context, nextPopup.OnButton1, nextPopup.OnButton2, nextPopup.OnButton3, nextPopup.OnClose);
            }
        };

        createdPopup.SetActive(true);
        Resources.UnloadUnusedAssets();
    }

    public void OpenCustomPopup(PopupType type, string title = "", string context = "", Action onButton1 = null, Action onButton2 = null, Action onButton3 = null, Action onClose = null)
    {
        Debugger.PrintLog("IsPopup On = " + isPopupOn);
        if (isPopupOn)
        {
            // 현재 팝업이 열려있으면 큐에 추가
            popupQueue.Enqueue(new PopupRequest(type, onButton1, onButton2, onButton3, onClose, title, context));
            return;
        }

        isPopupOn = true;

        // load popup
        var popup = Resources.Load<GameObject>("Prefab/Popup/" + type.ToString());

        if (popup == null)
        {
            Debugger.PrintLog(type.ToString() + " popup is missing!");
            return;
        }

        GameObject parentObj = FindObjectOfType<Canvas>().gameObject;

        if (parentObj == null)
        {
            Debugger.PrintLog("Empty Canvas", LogType.Error);
            return;
        }

        createdPopup = Instantiate(popup, parentObj.transform);
        var popupItem = createdPopup.GetComponent<PopupItem>();


        popupItem.SetText(title, context);
        popupItem.Button1 = onButton1;
        popupItem.Button2 = onButton2;
        popupItem.Button3 = onButton3;
        popupItem.Close = onClose;
        popupItem.End = () =>
        {
            isPopupOn = false;

            Debugger.PrintLog("Close Popup");
            if (createdPopup != null)
                Destroy(createdPopup);

            createdPopup = null;

            // 큐에서 다음 팝업 요청 처리
            if (popupQueue.Count > 0)
            {
                var nextPopup = popupQueue.Dequeue();
                OpenCustomPopup(nextPopup.Type, nextPopup.Title, nextPopup.Context, nextPopup.OnButton1, nextPopup.OnButton2, nextPopup.OnButton3, nextPopup.OnClose);
            }
        };

        createdPopup.SetActive(true);
        Resources.UnloadUnusedAssets();
    }

    public void CloseAllPopups()
    {
        if (createdPopup != null)
        {
            Destroy(createdPopup);
        }

        isPopupOn = false;
    }

    public bool isActivePopup()
    {
        return createdPopup != null;
    }

    public bool GetActivePopup()
    {
        return isPopupOn;
    }

    public void SetActivePopup(bool isPopupOn)
    {
        this.isPopupOn = isPopupOn;
    }

    private class PopupRequest
    {
        public PopupType Type { get; private set; }
        public Action OnButton1 { get; private set; }
        public Action OnButton2 { get; private set; }
        public Action OnButton3 { get; private set; }
        public Action OnClose { get; private set; }
        public string Title { get; private set; }
        public string Context { get; private set; }

        public PopupRequest(PopupType type, Action onButton1, Action onButton2, Action onButton3, Action onClose, string title = "", string context = "")
        {
            Type = type;
            OnButton1 = onButton1;
            OnButton2 = onButton2;
            OnButton3 = onButton3;
            OnClose = onClose;
            Title = title;
            Context = context;
        }
    }
}

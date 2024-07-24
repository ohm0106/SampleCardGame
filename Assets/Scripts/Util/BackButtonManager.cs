using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class BackButtonManager : MonoBehaviour
{
    [SerializeField] List<GameObject> panelsToClose;  // List of panels to close
    [SerializeField] GameObject lobbyPanel;

    private static Stack<GameObject> panelHistory = new Stack<GameObject>();

    void Awake()
    {
        panelHistory.Push(lobbyPanel); // init
    }

    public void OnBackButtonClicked()
    {
        if (panelHistory.Count > 0)
        {
            GameObject lastOpenedPanel = panelHistory.Pop();
            lastOpenedPanel.SetActive(false);
        }
        else
        {
            //todo : App Close
            Debugger.PrintLog("BackButtonManager: No panels to close.",LogType.Warning);
        }
    }

    public void OpenPanel(GameObject panel)
    {
        if (panelsToClose.Contains(panel))
        {
            panelHistory.Push(panel);
            panel.SetActive(true);
        }
        else
        {
            Debugger.PrintLog("BackButtonManager: Panel is not in the list of panels to close.",LogType.Warning);
        }
    }
}
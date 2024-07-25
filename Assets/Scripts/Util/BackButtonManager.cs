using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class BackButtonManager : MonoBehaviour
{
    [SerializeField] List<GameObject> panelsToClose;  
    [SerializeField] GameObject lobbyPanel;

    private Stack<GameObject> panelHistory = new Stack<GameObject>();

    void Awake()
    {
        panelHistory.Push(lobbyPanel); 
    }

    public void OnBackButtonClicked()
    {
        if (panelHistory.Count > 1) 
        {
            GameObject lastOpenedPanel = panelHistory.Pop();
            lastOpenedPanel.SetActive(false);

            panelHistory.Peek().SetActive(true);
        }
        else
        {
            Debugger.PrintLog("BackButtonManager: No panels to close.",LogType.Warning);
        }
    }

    public void OpenPanel(GameObject panel)
    {
        if (panelsToClose.Contains(panel))
        {
            panelHistory.Peek().SetActive(false);

            panelHistory.Push(panel);
            panel.SetActive(true);
        }
        else if (ReferenceEquals(lobbyPanel, panel))
        {
            while (panelHistory.Count > 0)
            {
                panelHistory.Pop().SetActive(false);
            }

            panelHistory.Clear();
            panelHistory.Push(panel);
            panel.SetActive(true);
        }
        else
        {
            Debugger.PrintLog("BackButtonManager: Panel is not in the list of panels to close.", LogType.Warning);
        }
    }
}
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class OpenPanelBtn : MonoBehaviour
{
    [SerializeField]
    GameObject openPanel;

    string openPanelName;

    [SerializeField]
    Button btn;

    private void Start()
    {
        if(btn == null)
        {
            btn = GetComponent<Button>();
        }
        btn.onClick.AddListener(Open);
    }

    void Open()
    {
        if (openPanel != null)
            SingletonManager.Instance.BackButtonManager.OpenPanel(openPanel);
        else
            SingletonManager.Instance.BackButtonManager.OpenPanel(openPanelName);
    }

    public void SetOepnPanel(GameObject panel)
    {
        openPanel = panel;
    }

    public void SetOpenPanelName(string name)
    {
        openPanelName = name;
    }


}

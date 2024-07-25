using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class OpenPanelBtn : MonoBehaviour
{
    [SerializeField]
    GameObject openPanel;

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
        SingletonManager.Instance.BackButtonManager.OpenPanel(openPanel);
    }

    public void SetOepnPanel(GameObject panel)
    {
        openPanel = panel;
    }

}

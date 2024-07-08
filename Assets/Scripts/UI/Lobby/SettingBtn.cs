using UnityEngine;
using UnityEngine.UI;
public class SettingBtn : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(ShowSetting);
    }


    void ShowSetting()
    {
        PopupManager.instance.Open(PopupType.Setting);
    }

}

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BackBtn : MonoBehaviour
{
    [SerializeField]
    Button btn;

    private void Start()
    {
        if (btn == null)
        {
            btn = GetComponent<Button>();
        }
        btn.onClick.AddListener(Back);
    }

    void Back()
    {
        SingletonManager.Instance.BackButtonManager.OnBackButtonClicked();
    }

}

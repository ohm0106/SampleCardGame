using UnityEngine;
using UnityEngine.UI;

public class StarUI : MonoBehaviour
{
    [SerializeField]
    Image ActiveStar;

    public void SetActiveStar(bool isActive)
    {
        if (ActiveStar.gameObject.activeSelf)
        {
            ActiveStar.gameObject.SetActive(false);

        }
        else
        {
            ActiveStar.gameObject.SetActive(true);
        }
    }
}

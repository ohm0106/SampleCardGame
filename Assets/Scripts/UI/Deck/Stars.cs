using UnityEngine;

public class Stars : MonoBehaviour
{
    StarUI[] starUIs;

    private void Awake()
    {
        starUIs = GetComponentsInChildren<StarUI>();
    }

    public void SetStars(int count)
    {
        if (count < 0 || count > starUIs.Length)
        {
            return;
        }

        for (int i = 0; i < starUIs.Length; i++)
        {
            if (i < count)
            {
                starUIs[i].SetActiveStar(true);  
            }
            else
            {
                starUIs[i].SetActiveStar(false); 
            }
        }
    }
}

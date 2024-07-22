using UnityEngine;
using TMPro;

public class CharacterCountUI : MonoBehaviour
{
    [SerializeField]
    TMP_Text count;

    public void SetCharacterCount(int curCount, int maxCount)
    {
        count.text = $"Heroes {curCount}/{maxCount}";
    }
}

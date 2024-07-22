using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum BadgeType { 

    Basic,
    Full

}
public class LevelSlider : MonoBehaviour
{
    [SerializeField]
    private TMP_Text levelText;

    [SerializeField]
    private TMP_Text statusText;

    [SerializeField]
    private Slider experienceBar;

    [SerializeField]
    private Image levelBadge;


    public void UpdateLevelUI(int level, int currentExperience, int maxExperience)
    {
        levelText.text = level.ToString();

        if (currentExperience >= maxExperience) 
        {
            statusText.text = "MAX!";
            experienceBar.value = 1;
        }
        else
        {
            statusText.text = $"{currentExperience}/{maxExperience}";

            // Update experience bar value
            float fillAmount = Mathf.Clamp01((float)currentExperience / maxExperience);
            experienceBar.value = fillAmount;
        }

        BadgeType badgeType = level >= 20 ? BadgeType.Full : BadgeType.Basic;

        levelBadge.sprite = ResourceLibrary.Instance.CharacterLibrary.GetLevelBadge(badgeType);

    }

    public void Clear()
    {
        levelText.text = string.Empty;
        statusText.text = string.Empty;
    }
}

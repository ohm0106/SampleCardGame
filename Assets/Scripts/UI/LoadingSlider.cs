using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LoadingSlider : MonoBehaviour
{
    [SerializeField] Slider slider;  
    [SerializeField] TMP_Text txtProgress;
    private void Awake()
    {
        if (slider == null)
            slider = GetComponentInChildren<Slider>();

        if (txtProgress == null)
            txtProgress = GetComponentInChildren<TMP_Text>();
    }

    private void Start()
    {
        SetSlider(0);
    }
    public void SetSlider(float progress)
    {
        if (txtProgress != null)
            txtProgress.text = $"Loading...%{progress * 100:0}%"; 

        if (slider != null)
            slider.value = progress; 
    }

    public void SetSlider(float progress, string status = "")
    {
        if (txtProgress != null)
            txtProgress.text = $"Loading... {progress * 100:0}%";

        if (slider != null)
            slider.value = progress;

    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LoadingSlider : MonoBehaviour
{
    [SerializeField] Slider slider;  
    [SerializeField] TMP_Text txt;   

    private void Awake()
    {
        if (slider == null)
            slider = GetComponentInChildren<Slider>();

        if (txt == null)
            txt = GetComponentInChildren<TMP_Text>();
    }

    public void SetSlider(float progress)
    {
        if (txt != null)
            txt.text = $"Loading... {progress * 100:0}%"; 

        if (slider != null)
            slider.value = progress; 
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class EnergyUI : MonoBehaviour
{
    [SerializeField]
    TMP_Text energyText;

    [SerializeField]
    Button plusBtn;

    private void OnEnable()
    {
        SingletonManager.Instance.Inventory.onAddEnergy += UpdateEnergyText;
        UpdateEnergyText(SingletonManager.Instance.Inventory.GetCurInventoryData().Currency.Energy);
    }

    private void OnDisable()
    {
        SingletonManager.Instance.Inventory.onAddEnergy -= UpdateEnergyText;
    }

    void UpdateEnergyText(int energy)
    {
        energyText.text = $"{energy} / 60";
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GemUI : MonoBehaviour
{
    [SerializeField]
    TMP_Text gemText;

    [SerializeField]
    Button plusBtn;

    private void OnEnable()
    {
        SingletonManager.Instance.Inventory.onAddGem += UpdateGemText;
        UpdateGemText(SingletonManager.Instance.Inventory.GetCurInventoryData().Currency.Gems);
    }

    private void OnDisable()
    {
        SingletonManager.Instance.Inventory.onAddGem -= UpdateGemText;
    }

    void UpdateGemText(int energy)
    {
        gemText.text = $"{CurrencyFormatter.FormatCurrency(energy)}";
    }
}

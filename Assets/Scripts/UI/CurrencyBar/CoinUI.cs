using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinUI : MonoBehaviour
{
    [SerializeField]
    TMP_Text coinText;

    [SerializeField]
    Button plusBtn;

    private void OnEnable()
    {
        SingletonManager.Instance.Inventory.onAddEnergy += UpdateCoinText;
        UpdateCoinText(SingletonManager.Instance.Inventory.GetCurInventoryData().Currency.Coins);
    }

    private void OnDisable()
    {
        SingletonManager.Instance.Inventory.onAddEnergy -= UpdateCoinText;
    }

    void UpdateCoinText(int coin)
    {
        coinText.text = $"{CurrencyFormatter.FormatCurrency(coin)}";
    }
}

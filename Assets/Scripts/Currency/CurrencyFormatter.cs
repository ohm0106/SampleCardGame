using UnityEngine;
using TMPro;

public static class CurrencyFormatter 
{
    public static string FormatCurrency(int amount)
    {
        if (amount >= 1000000000000)
        {
            return (amount / 1000000000000f).ToString("0.##") + "T";
        }
        else if (amount >= 1000000000)
        {
            return (amount / 1000000000f).ToString("0.##") + "B";
        }
        else if (amount >= 1000000)
        {
            return (amount / 1000000f).ToString("0.##") + "M";
        }
        else if (amount >= 1000)
        {
            return (amount / 1000f).ToString("0.##") + "K";
        }
        else
        {
            return amount.ToString("N0");
        }
    }

    private static string FormatWithCommas(string value)
    {
        if (value.Contains("."))
        {
            var parts = value.Split('.');
            parts[0] = int.Parse(parts[0]).ToString("N0");
            return string.Join(".", parts);
        }
        return int.Parse(value).ToString("N0");
    }
}

using UnityEngine;
using TMPro;

public static class CurrencyFormatter 
{
    public static string FormatCurrency(long amount)
    {
        return string.Format("{0:#,###}", amount);
    }

    private static string FormatWithCommas(float value)
    {
        string[] parts = value.ToString("0.##").Split('.');
        parts[0] = int.Parse(parts[0]).ToString("N0");

        if (parts.Length > 1)
        {
            return string.Join(".", parts);
        }
        return parts[0];
    }
}

using System;
using System.Collections.Generic;

[Serializable]
public class InventoryData
{
    public List<Item> Items { get; set; }

    public CurrencyData Currency { get; set; } 

    public InventoryData()
    {
        Items = new List<Item>();
        Currency = new CurrencyData();
    }
}


using System;
using System.Collections.Generic;

[Serializable]
public class Item
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public GradeType GradeType { get; set; }
}

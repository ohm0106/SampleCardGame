using UnityEngine;

public class StatIconLibrary : MonoBehaviour
{
    [SerializeField]
    StatIcon[] icons;


    public StatIcon GetStatIcon(StatType type)
    {
        foreach (var icon in icons)
        {
            if (icon.type == type)
            {
                return icon;
            }
        }
        return null;
    }
}

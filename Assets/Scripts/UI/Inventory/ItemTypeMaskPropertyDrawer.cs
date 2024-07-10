using System;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ItemTypeMask))]
public class ItemTypeMaskPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty maskProperty = property.FindPropertyRelative("mask");

        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Get current mask value
        int maskValue = maskProperty.intValue;

        // Draw the EnumFlagsField
        maskValue = (int)(ItemType)EditorGUI.EnumFlagsField(position, (ItemType)maskValue);

        // If changed, update the mask value
        if (maskProperty.intValue != maskValue)
        {
            maskProperty.intValue = maskValue;
            Debug.Log($"New mask value set: {maskProperty.intValue}");
        }

        EditorGUI.EndProperty();
    }
}
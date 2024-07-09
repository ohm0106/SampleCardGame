using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InventoryDebugger))]
public class InventoryDebuggerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        InventoryDebugger debugger = (InventoryDebugger)target;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Item Debugging", EditorStyles.boldLabel);

        string[] itemNames = new string[CreateItemSO.items.Count];
        for (int i = 0; i < CreateItemSO.items.Count; i++)
        {
            itemNames[i] = CreateItemSO.items[i].itemName;
        }

        debugger.selectedItemIndex = EditorGUILayout.Popup("Select Item", debugger.selectedItemIndex, itemNames);
        debugger.newItemQuantity = EditorGUILayout.IntField("Item Quantity", debugger.newItemQuantity);


        if (GUILayout.Button("Add Item"))
        {
            debugger.AddSelectedItem();
        }

        if (GUILayout.Button("Remove Item"))
        {
            debugger.RemoveSelectedItem();
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Inventory Actions", EditorStyles.boldLabel);

        if (GUILayout.Button("Save Inventory"))
        {
            debugger.SaveInventory();
        }

        if (GUILayout.Button("Load Inventory"))
        {
            debugger.LoadInventory();
        }

        if (GUILayout.Button("reset Inventory"))
        {
            debugger.ResetInventroy();
        }
    }
}

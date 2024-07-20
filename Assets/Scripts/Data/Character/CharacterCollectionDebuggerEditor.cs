using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[CustomEditor(typeof(CharacterCollectionDebugger))]
public class CharacterCollectionDebuggerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CharacterCollectionDebugger debugger = (CharacterCollectionDebugger)target;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Character Debugging", EditorStyles.boldLabel);

        // Assets/SO/Characters 폴더에서 모든 CharacterBaseSO 파일 검색
        string[] guids = AssetDatabase.FindAssets("t:CharacterBaseSO", new[] { "Assets/SO/Characters" });
        List<CharacterBaseSO> characterBaseList = guids.Select(guid => AssetDatabase.LoadAssetAtPath<CharacterBaseSO>(AssetDatabase.GUIDToAssetPath(guid))).ToList();
        List<string> characterNames = characterBaseList.Select(characterBase => characterBase.name).ToList();

        int selectedIndex = characterNames.IndexOf(debugger.selectedCharacterBaseSOName);
        selectedIndex = EditorGUILayout.Popup("Select Character Base SO", selectedIndex, characterNames.ToArray());
        if (selectedIndex >= 0 && selectedIndex < characterNames.Count)
        {
            debugger.selectedCharacterBaseSOName = characterNames[selectedIndex];
        }

        debugger.newCharacterDeckCount = EditorGUILayout.IntField("Character Deck Count", debugger.newCharacterDeckCount);

        if (GUILayout.Button("Add Character"))
        {
            debugger.AddCharacter();
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Deck Data Actions", EditorStyles.boldLabel);

        if (GUILayout.Button("Select Deck Data File"))
        {
            string path = EditorUtility.OpenFilePanel("Select Deck Data JSON", Application.dataPath, "json");
        }

        if (GUILayout.Button("Save Characters"))
        {
            debugger.SaveCharacters();
        }

        if (GUILayout.Button("Load Characters"))
        {
            debugger.LoadCharacters();
        }
        if(GUILayout.Button("Remove All"))
        {
            debugger.RemoveAllCharacters();
        }
    }
}

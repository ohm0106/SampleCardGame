using UnityEngine;

using UnityEngine;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CharacterLibrary : MonoBehaviour
{
    private Dictionary<string, CharacterBaseSO> characterDictionary = new Dictionary<string, CharacterBaseSO>();


    [System.Serializable]
    public class SlotImg
    {
        public GradeType type;
        public Sprite sprite;
    }

    public class CharacterTypeIcon
    {
        public CharacterType type;
        public Sprite sprite;
    }

    [SerializeField]
    SlotImg[] slotImg;

    [SerializeField]
    CharacterTypeIcon[] typeIcon;

    public async Task LoadAllItemsAsync(Action<float> onProgress)
    {
        if (characterDictionary.Count > 0)
        {
            onProgress?.Invoke(1.0f);
            return;
        }

        characterDictionary = new Dictionary<string, CharacterBaseSO>();

        AsyncOperationHandle<IList<CharacterBaseSO>> handle = Addressables.LoadAssetsAsync<CharacterBaseSO>("CharacterGroup");
        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            IList<CharacterBaseSO> temps = handle.Result;
            int totalCharacters = temps.Count;
            for (int i = 0; i < totalCharacters; i++)
            {
                CharacterBaseSO temp = temps[i];
                if (temp != null)
                {
                    characterDictionary[temp.name] = temp;
                }

                onProgress?.Invoke((float)(i + 1) / totalCharacters);

                await Task.Yield();
            }
        }
        else
        {
            Debugger.PrintLog($"Failed to load items: {handle.OperationException}", LogType.Warning);
        }
    }

    public CharacterBaseSO GetCharacterBase(string charactername)
    {
        if (characterDictionary.TryGetValue(charactername, out CharacterBaseSO character))
        {
            return character;
        }

        Debugger.PrintLog($"Item '{charactername}' not found in the library.", LogType.Warning);
        return null;
    }

    public Sprite GetCharacterImg(string charactername)
    {
        CharacterBaseSO character = GetCharacterBase(charactername);

        return Resources.Load<Sprite>(character.spritePath);
    }
    public List<CharacterBaseSO> GetCharacterBaseList()
    {
        return new List<CharacterBaseSO>(characterDictionary.Values);
    }

    public Sprite GetSlotImg(string charactername)
    {
        CharacterBaseSO character = GetCharacterBase(charactername);
        foreach (var arr in slotImg)
        {
            if (arr.type == character.gradeType)
            {
                return arr.sprite;
            }
        }

        return null;
    }

    public Sprite GetCharacterTypeIcon(string charactername)
    {
        CharacterBaseSO character = GetCharacterBase(charactername);
        foreach (var arr in typeIcon)
        {
            if (arr.type == character.type)
            {
                return arr.sprite;
            }
        }

        return null;
    }
}
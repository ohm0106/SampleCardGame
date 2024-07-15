using UnityEngine;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ItemLibrary : MonoBehaviour
{
    private Dictionary<string, ItemSO> itemDictionary = new Dictionary<string, ItemSO>();


    [System.Serializable]
    public class SlotImg
    {
        public GradeType type;
        public Sprite sprite;
    }

    [SerializeField]
    SlotImg[] slotImg;


    public async Task LoadAllItemsAsync(Action<float> onProgress)
    {
        if (itemDictionary.Count > 0)
        {
            onProgress?.Invoke(1.0f);
            return;
        }

        itemDictionary = new Dictionary<string, ItemSO>();

        AsyncOperationHandle<IList<ItemSO>> handle = Addressables.LoadAssetsAsync<ItemSO>("ItemGroup");
        await handle.Task; 

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            IList<ItemSO> items = handle.Result; 
            int totalItems = items.Count;
            for (int i = 0; i < totalItems; i++)
            {
                ItemSO item = items[i];
                if (item != null)
                {
                    itemDictionary[item.itemName] = item;
                }

                onProgress?.Invoke((float)(i + 1) / totalItems);

                await Task.Yield();
            }
        }
        else
        {
            Debugger.PrintLog($"Failed to load items: {handle.OperationException}", LogType.Warning);
        }
    }

    public ItemSO GetItem(string itemName)
    {
        if (itemDictionary.TryGetValue(itemName, out ItemSO item))
        {
            return item;
        }

        Debugger.PrintLog($"Item '{itemName}' not found in the library.",LogType.Warning);
        return null;
    }

    public Sprite GetSlotImg(GradeType type)
    {
        foreach (var arr in slotImg)
        {
            if (arr.type == type)
            {
                return arr.sprite;
            }
        }

        return null;
    }


}

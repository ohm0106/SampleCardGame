using UnityEngine;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ItemLibrary : MonoBehaviour
{
    private static ItemLibrary _instance;

    public static ItemLibrary Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ItemLibrary>();
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(nameof(ItemLibrary));
                    _instance = singletonObject.AddComponent<ItemLibrary>();
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return _instance;
        }
    }

    private Dictionary<string, ItemSO> itemDictionary = new Dictionary<string, ItemSO>();

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

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

                // 진행률 콜백을 호출합니다.
                onProgress?.Invoke((float)(i + 1) / totalItems);

                // UI 업데이트를 허용하기 위해 Yield합니다.
                await Task.Yield();
            }
        }
        else
        {
            Debug.LogError($"Failed to load items: {handle.OperationException}");
        }
    }

    public ItemSO GetItem(string itemName)
    {
        if (itemDictionary.TryGetValue(itemName, out ItemSO item))
        {
            return item;
        }
        Debug.LogWarning($"Item '{itemName}' not found in the library.");
        return null;
    }
}

using UnityEngine;

public class ResourceLibrary : MonoBehaviour
{
    private static ResourceLibrary _instance;
    private static readonly object _lock = new object();
    private static bool _applicationIsQuitting = false;

    public static ResourceLibrary Instance
    {
        get
        {
            if (_applicationIsQuitting)
            {
                Debug.LogWarning("[Singleton] Instance '" + typeof(ResourceLibrary) +
                    "' already destroyed on application quit." +
                    " Won't create again - returning null.");
                return null;
            }

            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<ResourceLibrary>();
                    if (_instance == null)
                    {
                        GameObject singletonObject = Instantiate(Resources.Load<GameObject>("ResourceManager"));
                        if (singletonObject != null)
                        {
                            _instance = singletonObject.GetComponent<ResourceLibrary>();
                            if (_instance == null)
                            {
                                _instance = singletonObject.AddComponent<ResourceLibrary>();
                            }
                            DontDestroyOnLoad(singletonObject);
                        }
                        else
                        {
                            Debug.LogError("ResourceManager prefab not found in Resources.");
                        }
                    }
                }
                return _instance;
            }
        }
    }

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

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _applicationIsQuitting = true;
        }
    }

    private ItemLibrary _itemLibrary;

    public ItemLibrary ItemLibrary
    {
        get
        {
            if (_itemLibrary == null)
            {
                _itemLibrary = gameObject.GetComponent<ItemLibrary>();
                if (_itemLibrary == null)
                {
                    _itemLibrary = gameObject.AddComponent<ItemLibrary>();
                }
            }
            return _itemLibrary;
        }
    }


    private StatIconLibrary _statIconLibrary;

    public StatIconLibrary StatIconLibrary
    {
        get
        {
            if (_statIconLibrary == null)
            {
                _statIconLibrary = gameObject.GetComponent<StatIconLibrary>();
                if (_statIconLibrary == null)
                {
                    _statIconLibrary = gameObject.AddComponent<StatIconLibrary>();
                }
            }
            return _statIconLibrary;
        }
    }
    private CharacterLibrary _characterLibrary;

    public CharacterLibrary CharacterLibrary
    {
        get
        {
            if (_characterLibrary == null)
            {
                _characterLibrary = gameObject.GetComponent<CharacterLibrary>();
                if (_characterLibrary == null)
                {
                    _characterLibrary = gameObject.AddComponent<CharacterLibrary>();
                }
            }
            return _characterLibrary;
        }
    }
}

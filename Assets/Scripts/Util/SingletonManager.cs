using UnityEngine;
public class SingletonManager : MonoBehaviour
{

    private static SingletonManager _instance;
    private static readonly object _lock = new object();
    private static bool _applicationIsQuitting = false;

    public static SingletonManager Instance
    {
        get
        {
            if (_applicationIsQuitting)
            {
                Debug.LogWarning("[Singleton] Instance '" + typeof(SingletonManager) +
                    "' already destroyed on application quit." +
                    " Won't create again - returning null.");
                return null;
            }

            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<SingletonManager>();
                    if (_instance == null)
                    {
                        GameObject temp = (GameObject)Resources.Load("SingletonManager");
                        if (temp != null)
                        {
                            GameObject singletonObject = Instantiate(temp);
                            _instance = singletonObject.GetComponent<SingletonManager>();
                            if (_instance == null)
                            {
                                _instance = singletonObject.AddComponent<SingletonManager>();
                            }
                            DontDestroyOnLoad(singletonObject);
                        }
                        else
                        {
                            Debug.LogError("SingletonManager prefab not found in Resources.");
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
            InitializeManagers();
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


    private LoadSceneManager _loadSceneManager;

    public LoadSceneManager LoadSceneManager
    {
        get
        {
            if (_loadSceneManager == null)
            {
                _loadSceneManager = gameObject.GetComponent<LoadSceneManager>();
                if (_loadSceneManager == null)
                {
                    _loadSceneManager = gameObject.AddComponent<LoadSceneManager>();
                }
            }
            return _loadSceneManager;
        }
    }

    private Inventory _inventory;

    public Inventory Inventory
    {
        get
        {
            if (_inventory == null)
            {
                _inventory = gameObject.GetComponent<Inventory>();
                if (_inventory == null)
                {
                    _inventory = gameObject.AddComponent<Inventory>();
                }
            }
            return _inventory;
        }
    }

    private void InitializeManagers()
    {
        // LoadSceneManager와 Inventory 초기화
        _loadSceneManager = LoadSceneManager;
        _inventory = Inventory;
       
    }
}
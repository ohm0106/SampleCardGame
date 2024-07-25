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
                 Debugger.PrintLog("[Singleton] Instance '" + typeof(SingletonManager) +
                    "' already destroyed on application quit." +
                    " Won't create again - returning null.", LogType.Warning);
                return null;
            }

            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = FindFirstObjectByType<SingletonManager>();
                    if (_instance == null)
                    {
                        GameObject temp = Resources.Load<GameObject>("SingletonManager");
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
                            Debugger.PrintLog("SingletonManager prefab not found in Resources.",LogType.Error);
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

    private void OnApplicationQuit()
    {
        _applicationIsQuitting = true;
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

    private CharacterCollection _characterCollection;

    public CharacterCollection CharacterCollection
    {
        get
        {
            if (_characterCollection == null)
            {
                _characterCollection = gameObject.GetComponent<CharacterCollection>();
                if (_characterCollection == null)
                {
                    _characterCollection = gameObject.AddComponent<CharacterCollection>();
                }
            }
            return _characterCollection;
        }
    }

    private void InitializeManagers()
    {
        _loadSceneManager = LoadSceneManager;
        _inventory = Inventory;
        _characterCollection = CharacterCollection;
    }

    private BackButtonManager _backButtonManager;

    public BackButtonManager BackButtonManager
    {
        get
        {
            if(_backButtonManager == null)
            {
                _backButtonManager = FindFirstObjectByType<BackButtonManager>();
            }
            return _backButtonManager;
        }
    }

}

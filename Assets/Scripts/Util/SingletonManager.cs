using UnityEngine;

public class SingletonManager : MonoBehaviour
{
    private static SingletonManager _instance;

    public static SingletonManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SingletonManager>();
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(nameof(SingletonManager));
                    _instance = singletonObject.AddComponent<SingletonManager>();
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return _instance;
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

    LoadSceneManager _loadSceneManager;

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

    Inventory _inventory;

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

    public void InitializeManagers()
    {
        
    }
}

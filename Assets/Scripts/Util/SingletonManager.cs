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
                    GameObject temp = (GameObject)Resources.Load("SingletonManager");
                    GameObject singletonObject = Instantiate(temp);
                    _instance = singletonObject.AddComponent<SingletonManager>();
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return _instance;
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
    private void InitializeManagers()
    {
        // LoadSceneManager와 Inventory 초기화
        _loadSceneManager = LoadSceneManager;
        _inventory = Inventory;
       
    }
}
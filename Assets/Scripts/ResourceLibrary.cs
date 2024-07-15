using UnityEngine;

public class ResourceLibrary : MonoBehaviour
{
    private static ResourceLibrary _instance;

    public static ResourceLibrary Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ResourceLibrary>();
                if (_instance == null)
                {
                    GameObject temp = (GameObject)Resources.Load("ResourceManager");
                    GameObject singletonObject = Instantiate(temp);
                    _instance = singletonObject.AddComponent<ResourceLibrary>();
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

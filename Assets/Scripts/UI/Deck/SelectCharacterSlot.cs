using UnityEngine;

public class SelectCharacterSlot : BaseSlot
{
    bool isLock = false;
    public override void ClearSlot()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateSlot(object data)
    {
        string status = data as string;
        if (data.Equals("0"))
        {
            isLock = false;
            GameObject temp = Instantiate(Resources.Load<GameObject>("Prefab/UI/AddSlot"), this.gameObject.transform);
            return;
        }
        else if (data.Equals("-1"))
        {
            isLock = true;
            GameObject temp = Instantiate(Resources.Load<GameObject>("Prefab/UI/LockSlot"), this.gameObject.transform);
        }
        else
        {
            GameObject temp = Instantiate(Resources.Load<GameObject>("Prefab/UI/SelectSlot"), this.gameObject.transform);
            temp.GetComponent<CharacterSlot>().UpdateSlot(SingletonManager.Instance.CharacterCollection.GetCharacter(status));
        }
    }
    
    public bool GetLock()
    {
        return isLock;
    }
}

[SerializeField]
public class PlayerDecks
{
    public SelectDeckInfo[] info;

    public PlayerDecks()
    {
        info = new SelectDeckInfo[]
        {
            new SelectDeckInfo { count = 1, id = "0" },
            new SelectDeckInfo { count = 2, id = "0" },
            new SelectDeckInfo { count = 3, id = "0" },
            new SelectDeckInfo { count = 4, id = "-1" },
            new SelectDeckInfo { count = 5, id = "-1" }
        };
    }
}


[SerializeField]
public class SelectDeckInfo
{
    public string id;

    public int count;
}
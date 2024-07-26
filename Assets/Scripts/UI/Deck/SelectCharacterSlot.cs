using UnityEngine;

public class SelectCharacterSlot : BaseSlot
{
    bool isLock = false;
    GameObject slot; 
    public override void ClearSlot()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateSlot(object data)
    {
        string status = data as string;

        if(slot != null)
        {
            Destroy(slot);
            slot = null;
        }

        if (data.Equals("0"))
        {
            isLock = false;
            slot = Instantiate(Resources.Load<GameObject>("Prefab/UI/AddSlot"), this.gameObject.transform);
            slot.GetComponent<OpenPanelBtn>().SetOpenPanelName("Character_List");
            return;
        }
        else if (data.Equals("-1"))
        {
            isLock = true;
            slot =  Instantiate(Resources.Load<GameObject>("Prefab/UI/LockSlot"), this.gameObject.transform);
        }
        else
        {
            slot = Instantiate(Resources.Load<GameObject>("Prefab/UI/SelectSlot"), this.gameObject.transform);
            slot.GetComponent<CharacterSlot>().UpdateSlot(SingletonManager.Instance.CharacterCollection.GetCharacter(status));
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
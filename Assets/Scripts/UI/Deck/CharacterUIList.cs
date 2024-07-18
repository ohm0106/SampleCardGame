using UnityEngine;

public class CharacterUIList : MonoBehaviour
{
    int listSize = 50;


    CharacterSlot[] slots;

    [SerializeField]
    GameObject slotPrefab;

    [SerializeField]
    Transform scrollContent;

    void OnEnable()
    {
        InitializeSlots();
        
    }

    void OnDisable()
    {
        ClearAllSlots();
    }

    void InitializeSlots()
    {
        if (slots != null)
            return;

        slots = new CharacterSlot[listSize];

        var charactersInfo = SingletonManager.Instance.CharacterCollection.GetCharacterList();

        for (int i = 0; i < listSize; i++)
        {
            GameObject slotObj = Instantiate(slotPrefab, scrollContent);
            slots[i] = slotObj.GetComponent<CharacterSlot>();

            if (i < charactersInfo.Count)
            {
                Character temp = charactersInfo[i];
                slots[i].UpdateSlot(temp);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }

    }

    void ClearAllSlots()
    {
        foreach (var slot in slots)
        {
            slot.ClearSlot();
        }
    }
}

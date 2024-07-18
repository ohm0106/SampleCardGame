using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterSlot : MonoBehaviour
{
    string udid;
    [SerializeField]
    TMP_Text characterName;
    TMP_Text level;

    [SerializeField]
    Image characterTypeIconImg;
    [SerializeField]
    Image characterImg;
    [SerializeField]
    Image slotImg;

    int starCount;
    // todo : star 
    bool isRelease;

    Sprite defaultSlot = default;

    private void Start()
    {
        defaultSlot = slotImg.sprite;
    }
    private void OnEnable()
    {
        
    }

    public void UpdateSlot(Character character)
    {
        udid = character.id;
        this.characterName.text = character.name;
        characterImg.sprite = ResourceLibrary.Instance.CharacterLibrary.GetCharacterImg(character.name);
        characterTypeIconImg.sprite = ResourceLibrary.Instance.CharacterLibrary.GetCharacterTypeIcon(character.name);
        slotImg.sprite = ResourceLibrary.Instance.CharacterLibrary.GetSlotImg(character.name);

        isRelease = true;

        starCount = character.star;
        level.text = character.level.ToString();
        // star set
    } 

    public void ClearSlot()
    {
        udid = string.Empty;
        characterImg.sprite = null;
        characterTypeIconImg.sprite = null;
        slotImg.sprite = defaultSlot;
        isRelease = false;

        starCount = 0;
        // star release 
    }

    public string GetUDID()
    {
        return udid;
    }
    
}


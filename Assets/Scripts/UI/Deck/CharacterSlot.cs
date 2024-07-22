using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterSlot : MonoBehaviour 
{
    string udid;
    [SerializeField]
    TMP_Text characterName;
    [SerializeField]
    TMP_Text level;

    [SerializeField]
    Image characterTypeIconImg;
    [SerializeField]
    Image characterImg;
    [SerializeField]
    Image slotImg;
    [SerializeField]
    LevelSlider levelSlider;

    bool isEmpty;
  //  Sprite defaultSlot = default;

    [SerializeField]
    Stars starUIs;
    private void Start()
    {
        udid = string.Empty;
        levelSlider = GetComponent<LevelSlider>();
    }

    public void UpdateSlot(Character character)
    {
        SetActiveStateForChildren(true);

        udid = character.GetGUID();
        this.characterName.text = character.name;
        characterImg.sprite = ResourceLibrary.Instance.CharacterLibrary.GetCharacterImg(character.name);
        characterTypeIconImg.sprite = ResourceLibrary.Instance.CharacterLibrary.GetCharacterTypeIcon(character.name);
        slotImg.sprite = ResourceLibrary.Instance.CharacterLibrary.GetSlotImg(character.name);

        isEmpty = false;

        level.text = character.level.ToString();

        levelSlider.UpdateLevelUI(character.level, character.experience, 9); // todo : 경험치 총량은 Static Data 에서 불러오도록 할 것! 
        starUIs.SetStars(character.star);
        AdjustImageWidth();
    }

    public void ClearSlot()
    {
        udid = string.Empty;
        characterImg.sprite = null;
        characterTypeIconImg.sprite = null;
       // slotImg.sprite = defaultSlot;
        isEmpty = true;
        starUIs.SetStars(0);
        levelSlider.Clear();

        SetActiveStateForChildren(false);
    }

    public string GetGUID()
    {
        return udid;
    }

    public bool GetSlotEmptyStatus()
    {
        return isEmpty;
    }


    private void AdjustImageWidth()
    {
        if (characterImg.sprite == null)
            return;

        RectTransform rt = characterImg.GetComponent<RectTransform>();
        float aspectRatio = characterImg.sprite.bounds.size.x / characterImg.sprite.bounds.size.y;

        float fixedHeight = rt.rect.height;
        rt.sizeDelta = new Vector2(fixedHeight * aspectRatio, fixedHeight);
    }
    void SetActiveStateForChildren(bool state)
    {
        Transform[] children = GetComponentsInChildren<Transform>(true);
        foreach (var child in children)
        {
            if (child != this.transform) 
            {
                child.gameObject.SetActive(state);
            }
        }
    }

}

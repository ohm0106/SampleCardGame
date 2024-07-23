using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterSlot : BaseSlot
{
    string udid;
    [SerializeField]
    TMP_Text characterName;
    [SerializeField]
    Image characterTypeIconImg;
    [SerializeField]
    Image characterImg;
    [SerializeField]
    LevelSlider levelSlider;
    [SerializeField]
    Stars starUIs;

    private void Start()
    {
        udid = string.Empty;
        levelSlider = GetComponent<LevelSlider>();
    }

    public override void UpdateSlot(object data)
    {
        Character character = data as Character;
        if (character == null)
        {
            Debug.LogError("Invalid character data");
            return;
        }

        SetActiveStateForChildren(true);

        udid = character.GetGUID();
        this.characterName.text = character.name;
        characterImg.sprite = ResourceLibrary.Instance.CharacterLibrary.GetCharacterImg(character.name);
        characterTypeIconImg.sprite = ResourceLibrary.Instance.CharacterLibrary.GetCharacterTypeIcon(character.name);
        slotImg.sprite = ResourceLibrary.Instance.CharacterLibrary.GetSlotImg(character.name);

        isEmpty = false;
        levelSlider.UpdateLevelUI(character.level, character.experience, 9); // todo : 경험치 총량은 Static Data 에서 불러오도록 할 것! 
        starUIs.SetStars(character.star);
        AdjustImageWidth();
    }

    public override void ClearSlot()
    {
        udid = string.Empty;
        characterImg.sprite = null;
        characterTypeIconImg.sprite = null;
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
}

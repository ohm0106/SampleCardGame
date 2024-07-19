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

    int starCount;
    bool isRelease;

    Sprite defaultSlot = default;

    private void Start()
    {
        defaultSlot = slotImg.sprite;
        udid = string.Empty;
    }

    public void UpdateSlot(Character character)
    {
        udid = character.GetGUID();
        this.characterName.text = character.name;
        characterImg.sprite = ResourceLibrary.Instance.CharacterLibrary.GetCharacterImg(character.name);
        characterTypeIconImg.sprite = ResourceLibrary.Instance.CharacterLibrary.GetCharacterTypeIcon(character.name);
        slotImg.sprite = ResourceLibrary.Instance.CharacterLibrary.GetSlotImg(character.name);

        isRelease = true;

        starCount = character.star;
        level.text = character.level.ToString();

        AdjustImageWidth();
    }

    public void ClearSlot()
    {
        udid = string.Empty;
        characterImg.sprite = null;
        characterTypeIconImg.sprite = null;
        slotImg.sprite = defaultSlot;
        isRelease = false;

        starCount = 0;
    }

    public string GetGUID()
    {
        return udid;
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

using System.Collections.Generic;
using UnityEngine;

public class CharacterUIList : BaseUIList<Character, CharacterSlot>
{
    protected override void SubscribeToEvents()
    {
        SingletonManager.Instance.CharacterCollection.onAddCharacter += UpdateCharacterUI;
        SingletonManager.Instance.CharacterCollection.onRemoveCharacter += DeleteCharacterUI;
    }

    protected override void UnsubscribeFromEvents()
    {
        SingletonManager.Instance.CharacterCollection.onAddCharacter -= UpdateCharacterUI;
        SingletonManager.Instance.CharacterCollection.onRemoveCharacter -= DeleteCharacterUI;
    }

    protected override List<Character> GetItems()
    {
        return SingletonManager.Instance.CharacterCollection.GetCharacterList();
    }
    protected override void ClearSlot(CharacterSlot slot)
    {
        slot.ClearSlot();
    }
    protected override void UpdateSlot(CharacterSlot slot, Character character)
    {
        slot.UpdateSlot(character);
    }
    protected override bool IsSlotFilled(CharacterSlot slot)
    {
        return !slot.GetGUID().Equals(string.Empty);
    }
    protected override int CompareSlots(CharacterSlot a, CharacterSlot b)
    {
        return b.GetGUID().Equals(a.GetGUID()) ? 1 : 0;
    }
    public void UpdateCharacterUI(Character newCharacter)
    {
        foreach (var slot in slots)
        {
            if (!slot.GetSlotEmptyStatus() && slot.GetGUID() == newCharacter.GetGUID())
            {
                slot.UpdateSlot(newCharacter);
                ReorganizeSlots();
                return;
            }
            else if (slot.GetSlotEmptyStatus() && slot.GetGUID().Equals(string.Empty))
            {
                slot.UpdateSlot(newCharacter);
                ReorganizeSlots();
                return;
            }
        }
    }

    public void DeleteCharacterUI(Character newCharacter)
    {
        foreach (var slot in slots)
        {
            if ( slot.GetGUID() == newCharacter.GetGUID())
            {
                slot.ClearSlot();
                ReorganizeSlots();
                break;
            }
        }
    }


}

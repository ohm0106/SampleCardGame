using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterSelectedUIList : BaseUIList<SelectDeckInfo, SelectCharacterSlot>
{

    protected override int CompareSlots(SelectCharacterSlot a, SelectCharacterSlot b)
    {
        return 0;
    }

    protected override List<SelectDeckInfo> GetItems()
    {
        return SingletonManager.Instance.CharacterCollection.GetPlayerDeckInfo().info.ToList();
    }

    protected override bool IsSlotFilled(SelectCharacterSlot slot)
    {
        return !slot.GetLock();
    }

    protected override void SubscribeToEvents()
    {
        throw new System.NotImplementedException();
    }

    protected override void UnsubscribeFromEvents()
    {
        throw new System.NotImplementedException();
    }

    protected override void UpdateSlot(SelectCharacterSlot slot, SelectDeckInfo info)
    {
        slot.UpdateSlot(info.id);
    }
    protected override void ClearSlot(SelectCharacterSlot slot)
    {
        throw new System.NotImplementedException();
    }

}

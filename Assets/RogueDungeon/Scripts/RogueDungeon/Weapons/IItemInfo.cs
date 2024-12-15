using UnityEngine;

namespace RogueDungeon.Behaviours.WeaponWielding
{
    public interface IItemInfo
    {
        float Weight { get; }
        Sprite Sprite { get; }
    }
}
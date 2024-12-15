using UnityEngine;

namespace RogueDungeon.Items
{
    public interface IItemInfo
    {
        float Weight { get; }
        Sprite Sprite { get; }
    }
}
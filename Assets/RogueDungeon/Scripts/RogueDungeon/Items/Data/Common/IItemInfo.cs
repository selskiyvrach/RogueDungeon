using UnityEngine;

namespace RogueDungeon.Items.Data.Common
{
    public interface IItemInfo
    {
        float Weight { get; }
        Sprite Sprite { get; }
    }
}
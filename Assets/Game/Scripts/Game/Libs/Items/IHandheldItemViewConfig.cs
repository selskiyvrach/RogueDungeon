using UnityEngine;

namespace Game.Libs.Items
{
    public interface IHandheldItemViewConfig : IItemConfig
    {
        Sprite Sprite { get; }
        Vector2Int Size { get; }
    }
}
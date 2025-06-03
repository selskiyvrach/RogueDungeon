using UnityEngine;

namespace Game.Features.Items.Domain
{
    public interface IHandheldItemViewConfig : IItemConfig
    {
        Sprite Sprite { get; }
        Vector2Int Size { get; }
    }
}
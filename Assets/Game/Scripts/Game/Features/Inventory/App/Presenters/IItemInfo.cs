using Game.Libs.UI;
using UnityEngine;

namespace Game.Features.Inventory.App.Presenters
{
    public interface IItemInfo : IIDable
    {
        Sprite Sprite { get; }
        Vector2Int Size { get; }
    }
}
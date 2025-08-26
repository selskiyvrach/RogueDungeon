using Game.Libs.UI;
using UnityEngine;

namespace Game.Features.Inventory.App.Presenters
{
    public interface IItemViewSetupArgs : IIDable
    {
        Sprite Sprite { get; }
        Vector2Int Size { get; }
    }
}
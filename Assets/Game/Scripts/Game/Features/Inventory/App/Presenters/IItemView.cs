using Game.Libs.UI;
using UnityEngine;

namespace Game.Features.Inventory.App.Presenters
{
    public interface IItemView : IHoverable, IIDable, IHoverDisplayable, IBeingDraggedDisplayable
    {
        void Setup(IItemInfo info);
        void SetCellSize(float cellSize);
        void SetParent(Transform parent);
        void SetLocalPosition(Vector2 pos);
    }
}
using Game.Libs.UI;
using UnityEngine;

namespace Game.Features.Inventory.App.Presenters
{
    public interface IItemView : IIDable, IHoverDisplayable, IBeingDraggedDisplayable, IRaycastable
    {
        IProjectionView ProjectionView { get; }
        void Setup(IItemViewSetupArgs viewSetupArgs);
        void SetCellSize(float cellSize);
        void SetParent(Transform parent);
        void SetLocalPosition(Vector2 pos);
        Vector2 GetScreenPosition(Camera camera);
    }
}
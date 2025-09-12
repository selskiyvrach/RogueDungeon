using System;
using Game.Libs.UI;
using UnityEngine;

namespace Game.Features.Inventory.App.Presenters
{
    public interface IItemView : IIDable, IHoverDisplayable, IRaycastable, IDisposable
    {
        IProjectionView ProjectionView { get; }
        void SetCellSize(float cellSize);
        void SetPosition(Vector3 pos);
        Vector2 GetScreenPosition(Camera camera);
        void SetParent(Transform parent);
        void RefreshSubElementsPositions();
    }
}
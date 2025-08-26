using System;
using UnityEngine;

namespace Game.Features.Inventory.App.Presenters
{
    public interface IInventoryInput
    {
        event Action OnPointerDown;
        event Action OnPointerUp;
        event Action OnMoved;
        Vector2 ScreenPosition { get; }
    }
}
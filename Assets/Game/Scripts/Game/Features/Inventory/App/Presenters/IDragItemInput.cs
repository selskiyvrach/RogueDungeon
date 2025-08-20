using System;

namespace Game.Features.Inventory.App.Presenters
{
    public interface IDragItemInput
    {
        event Action OnPointerDown;
        event Action OnPointerUp;
        event Action OnMoved;
    }
}
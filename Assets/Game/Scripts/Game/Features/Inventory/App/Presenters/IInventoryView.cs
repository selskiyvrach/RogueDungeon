using System;
using System.Collections.Generic;

namespace Game.Features.Inventory.App.Presenters
{
    public interface IInventoryView : IDisposable
    {
    }

    public interface IContainerView : IDisposable
    {
        event Action OnItemChanged;
        void SetItems();
    }
}
using System;
using Zenject;

namespace Game.Features.Inventory.App.Presenters
{
    public class InventoryPresenter : IDisposable, IInitializable
    {
        private readonly Domain.Inventory _model;
        private readonly IInventoryView _view;

        public InventoryPresenter(Domain.Inventory model, IInventoryView view)
        {
            _model = model;
            _view = view;
        }

        public void Dispose() => 
            _view.Dispose();

        public void Initialize()
        {
            // get all model containers
            // initialize view of the containers with items
            
            // when projected
            // on slot projection 
        }
    }
}
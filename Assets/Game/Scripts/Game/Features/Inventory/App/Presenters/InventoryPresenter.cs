using System;
using Zenject;

namespace Game.Features.Inventory.App.Presenters
{
    public class InventoryPresenter : IDisposable, IInitializable
    {
        private readonly Domain.Inventory _model;
        private readonly Mediator _mediator;

        public InventoryPresenter(Domain.Inventory model, Mediator mediator)
        {
            _model = model;
            _mediator = mediator;
        }

        public void Initialize()
        {

        }

        public void Dispose() => 
            _mediator.Dispose();
    }
}
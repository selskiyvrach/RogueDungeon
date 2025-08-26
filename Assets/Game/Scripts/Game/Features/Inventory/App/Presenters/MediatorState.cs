using System;

namespace Game.Features.Inventory.App.Presenters
{
    public abstract class MediatorState : IDisposable
    {
        protected Mediator Mediator { get; private set; }

        public void Init(Mediator mediator) => 
            Mediator = mediator;

        public abstract void Dispose();
    }
}
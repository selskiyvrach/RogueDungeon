namespace Game.Features.Inventory.App.Presenters
{
    public abstract class MediatorState
    {
        protected Mediator Mediator { get; private set; }

        public void Init(Mediator mediator) => 
            Mediator = mediator;
    }
}
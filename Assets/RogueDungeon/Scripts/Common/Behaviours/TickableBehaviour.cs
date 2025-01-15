namespace Common.Behaviours
{
    public abstract class TickableBehaviour : Behaviour
    {
        private readonly Ticker _ticker = new();

        public override void Enable()
        {
            base.Enable();
            _ticker.Start(Tick);
        }

        public override void Disable()
        {
            base.Disable();
            _ticker.Stop();
        }

        protected abstract void Tick(float timeDelta);
    }
}
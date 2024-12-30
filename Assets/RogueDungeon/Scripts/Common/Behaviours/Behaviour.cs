namespace Common.Behaviours
{
    public abstract class Behaviour : IBehaviour
    {
        private readonly Ticker _ticker = new();
        private bool _isEnabled;

        public bool IsEnabled => _isEnabled;

        public virtual void Enable()
        {
            _ticker.Start(Tick);
            _isEnabled = true;
        }

        public virtual void Disable()
        {
            _ticker.Stop();
            _isEnabled = false;
        }

        protected abstract void Tick(float timeDelta);
    }
}
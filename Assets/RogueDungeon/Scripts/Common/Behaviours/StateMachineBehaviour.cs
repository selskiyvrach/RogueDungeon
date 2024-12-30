using Common.Fsm;

namespace Common.Behaviours
{
    public abstract class StateMachineBehaviour : StateMachine, IBehaviour
    {
        private readonly Ticker _ticker = new();
        public bool IsEnabled { get; private set; }

        protected StateMachineBehaviour(IStatesFactory statesFactory, ILogger logger = null) : base(statesFactory, logger)
        {
        }

        public override void Enable()
        {
            base.Enable();
            _ticker.Start(Tick);
            IsEnabled = true;
        }

        public void Disable()
        {
            _ticker.Stop();
            IsEnabled = false;
        }
    }
}
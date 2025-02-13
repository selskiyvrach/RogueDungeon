using Common.Fsm;

namespace Common.Behaviours
{
    public class StateMachineBehaviour : IBehaviour
    {
        private readonly StateMachine _stateMachine;
        private readonly Ticker _ticker = new();
        public bool IsEnabled { get; private set; }

        protected StateMachineBehaviour(StateMachine stateMachine) => 
            _stateMachine = stateMachine;

        public void Enable()
        {
            _stateMachine.Enable();
            _ticker.Start(_stateMachine.Tick);
            IsEnabled = true;
        }

        public void Disable()
        {
            _ticker.Stop();
            IsEnabled = false;
        }
    }
}
using Common.Fsm;

namespace Common.Behaviours
{
    public class StateMachineBehaviour : IBehaviour
    {
        public readonly StateMachine StateMachine;
        private readonly Ticker _ticker = new();
        public bool IsEnabled { get; private set; }

        protected StateMachineBehaviour(StateMachine stateMachine) => 
            StateMachine = stateMachine;

        public virtual void Enable()
        {
            StateMachine.Enable();
            _ticker.Start(StateMachine.Tick);
            IsEnabled = true;
        }

        public void Disable()
        {
            _ticker.Stop();
            IsEnabled = false;
        }
    }
}
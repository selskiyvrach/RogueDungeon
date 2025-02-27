using Common.Fsm;
using Common.Time;

namespace Common.Behaviours
{
    public class StateMachineBehaviour : IBehaviour
    {
        private readonly Ticker _ticker = new();
        protected readonly StateMachine StateMachine;
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
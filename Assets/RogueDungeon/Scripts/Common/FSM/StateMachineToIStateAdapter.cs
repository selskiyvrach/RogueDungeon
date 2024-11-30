using Common.DebugTools;

namespace Common.FSM
{
    public class StateMachineToIStateAdapter : IState, IExitable, IEnterable, ITickable, IDebugName
    {
        private readonly StateMachine _stateMachine;

        public string DebugName
        {
            get => _stateMachine.DebugName;
            set => _stateMachine.DebugName = value;
        }

        public StateMachineToIStateAdapter(StateMachine stateMachine) => 
            _stateMachine = stateMachine;

        public void Exit() => 
            _stateMachine.Stop();

        public void Tick() => 
            _stateMachine.Tick();

        public void Enter()
        {
            
        }
    }
}
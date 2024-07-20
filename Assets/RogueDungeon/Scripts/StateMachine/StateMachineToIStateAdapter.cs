namespace RogueDungeon.StateMachine
{
    public class StateMachineToIStateAdapter : IState, IExitable, IEnterable, ITickable
    {
        private readonly StateMachine _stateMachine;

        public StateMachineToIStateAdapter(StateMachine stateMachine) => 
            _stateMachine = stateMachine;

        public void Exit() => 
            _stateMachine.Stop();

        public void Enter() => 
            _stateMachine.Run();

        public void Tick() => 
            _stateMachine.Tick();
    }
}
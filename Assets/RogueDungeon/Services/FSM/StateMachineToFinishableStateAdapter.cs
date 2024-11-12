namespace RogueDungeon.Services.FSM
{
    public class StateMachineToFinishableStateAdapter : StateMachineToIStateAdapter, IFinishableState
    {
        private readonly IFinishable _finishable;
        public bool IsFinished => _finishable.IsFinished;

        public StateMachineToFinishableStateAdapter(StateMachine stateMachine, IFinishable finishable) : base(stateMachine) => 
            _finishable = finishable;
    }
}
namespace RogueDungeon.StateMachine
{
    public class FinishableByOtherState : StateWithHandlers, IFinishable
    {
        private readonly IFinishable _finishable;

        public bool IsFinished => _finishable.IsFinished;

        public FinishableByOtherState(IFinishable finishable) => 
            _finishable = finishable;
    }

    public class FinishableByOtherState<T> : FinishableByOtherState where T : IFinishable
    {
        public FinishableByOtherState(T finishable) : base(finishable)
        {
        }
    }
}
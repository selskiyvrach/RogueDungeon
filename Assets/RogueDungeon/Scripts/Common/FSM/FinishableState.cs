namespace Common.FSM
{
    public class FinishableState : IState, IFinishable
    {
        private readonly IFinishable _finishable;

        public bool IsFinished => _finishable.IsFinished;
        
        public FinishableState(IFinishable finishable) => 
            _finishable = finishable;
    }
}
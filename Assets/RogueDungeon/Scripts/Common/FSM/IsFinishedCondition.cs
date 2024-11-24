namespace Common.FSM
{
    public class IsFinishedCondition : ICondition
    {
        private readonly IFinishable _finishable;

        public IsFinishedCondition(IFinishable finishable) => 
            _finishable = finishable;

        public bool IsMet() =>
            _finishable.IsFinished;
    }
}
namespace RogueDungeon.StateMachine
{
    public class IsCurrentStateFinishedCondition : ICondition
    {
        private readonly ICurrentStateProvider _currentStateProvider;

        public IsCurrentStateFinishedCondition(ICurrentStateProvider currentStateProvider) => 
            _currentStateProvider = currentStateProvider;

        public bool IsMet() =>
            ((IFinishable)_currentStateProvider.CurrentState).IsFinished;
    }
}
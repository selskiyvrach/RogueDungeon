namespace RogueDungeon.StateMachine
{
    public class IsInStateCondition : ICondition
    {
        private readonly ICurrentStateProvider _currentStateProvider;
        private readonly IState _targetState;

        public IsInStateCondition(ICurrentStateProvider currentStateProvider, IState targetState)
        {
            _currentStateProvider = currentStateProvider;
            _targetState = targetState;
        }

        public bool IsMet() => 
            _currentStateProvider.CurrentState == _targetState;
    }
}
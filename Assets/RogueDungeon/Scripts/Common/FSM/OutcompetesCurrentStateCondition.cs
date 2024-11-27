using System;

namespace Common.FSM
{
    public abstract class OutcompetesCurrentStateCondition<T> : ICondition where T : IComparable<T>
    {
        private readonly ICurrentStateProvider _currentStateProvider;
        private readonly ICompetingState<T> _state;
        
        public bool IsMet() =>
            _currentStateProvider.CurrentState is ICompetingState<T> competitor && _state.Competition.CompareTo(competitor.Competition) > 0;
    }
}
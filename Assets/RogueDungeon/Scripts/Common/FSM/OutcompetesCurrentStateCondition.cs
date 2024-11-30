using System;
using Common.Properties;

namespace Common.FSM
{
    public class OutcompetesCurrentStateCondition<T> : ICondition where T : IComparable<T>
    {
        private readonly IReadOnlyProperty<IState> _currentStateProvider;
        private readonly ICompetingState<T> _state;

        public OutcompetesCurrentStateCondition(ICompetingState<T> state, IReadOnlyProperty<IState> currentStateProvider)
        {
            _currentStateProvider = currentStateProvider;
            _state = state;
        }

        public bool IsMet() =>
            _currentStateProvider.Value is ICompetingState<T> competitor && _state.Competition.CompareTo(competitor.Competition) > 0;
    }
}
using System;
using Common.Providers;

namespace Common.FSM
{
    public class OutcompetesCurrentStateCondition<T> : ICondition where T : IComparable<T>
    {
        private readonly IProvider<IState> _currentStateProvider;
        private readonly ICompetingState<T> _state;

        public OutcompetesCurrentStateCondition(ICompetingState<T> state, IProvider<IState> currentStateProvider)
        {
            _currentStateProvider = currentStateProvider;
            _state = state;
        }

        public bool IsMet() =>
            _currentStateProvider.Get is ICompetingState<T> competitor && _state.Competition.CompareTo(competitor.Competition) > 0;
    }
}
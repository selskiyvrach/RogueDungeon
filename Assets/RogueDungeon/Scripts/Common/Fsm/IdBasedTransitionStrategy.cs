using System.Collections.Generic;
using System.Linq;

namespace Common.Fsm
{
    public class IdBasedTransitionStrategy : IStateTransitionStrategy
    {
        private readonly IState _startState;
        public Dictionary<string, IIdBasedTransitionableState> Moves { get; }

        public IdBasedTransitionStrategy(IEnumerable<IIdBasedTransitionableState> moves, string startState)
        {
            Moves = moves.ToDictionary(n => n.Id, n => n);
            _startState = Moves[startState];
        }

        public IState GetStartState() => 
            _startState;

        public IState GetTransition(IState state) => 
            ((IIdBasedTransitionableState)state).GetTransitionStateId() is { } id 
                ? Moves[id] 
                : null;
    }
}
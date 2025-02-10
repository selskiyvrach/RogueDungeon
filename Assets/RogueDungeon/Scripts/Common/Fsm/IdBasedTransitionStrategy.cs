using System.Collections.Generic;

namespace Common.Fsm
{
    public class IdBasedTransitionStrategy : IStateTransitionStrategy
    {
        public string StartStateId { get; set; }
        public Dictionary<string, IIdBasedTransitionableState> TransitionMap { get; set; }

        public IState GetStartState() => 
            TransitionMap[StartStateId];

        public IState GetTransition(IState state) => 
            ((IIdBasedTransitionableState)state).GetTransitionStateId() is { } id 
                ? TransitionMap[id] 
                : null;
    }
}
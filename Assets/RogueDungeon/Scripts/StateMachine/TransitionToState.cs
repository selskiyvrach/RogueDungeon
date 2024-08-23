using System;
using RogueDungeon.DebugTools;

namespace RogueDungeon.StateMachine
{
    public class TransitionToState : ITransition
    {
        private readonly IState _state;
        private readonly ICondition _condition;

        public TransitionToState(IState state, ICondition condition)
        {
            _state = state;
            _condition = condition;
        }

        public bool CanTransit(StatesContainer statesContainer, out IState transitionTo)
        {
            transitionTo = _condition.IsMet() 
                ? statesContainer.GetState(_state) ?? throw new Exception("States container does not contain this state: " + 
                                                                          (_state is IDebugName nameable ? nameable.DebugName : _state?.GetType().Name))
                : null;
            return transitionTo != null;
        }
    }
}
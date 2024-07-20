using System;
using System.Collections.Generic;

namespace RogueDungeon.StateMachine
{
    public class StatesContainer
    {
        private IState _startState;
        private readonly Dictionary<Type, IState> _states = new();

        public void SetStartState(IState startState) => 
            _startState = startState;

        public void AddState(IState state) => 
            _states[state.GetType()] = state;

        public IState GetStartState() => 
            _startState;

        public IState GetState(Type stateType) => 
            _states[stateType];
    }

    public class Transitions : List<Transition>
    {
    }

    public class TransitionsContainer
    {
        private readonly Transitions _transitions = new();

        public void Add(Transition transition) => 
            _transitions.Add(transition);

        public bool CanTransitTo(out Type stateType)
        {
            stateType = null;
            
            foreach (var transition in _transitions)
            {
                if(!transition.Condition.IsMet())
                    continue;
                stateType = transition.To;
                break;
            }

            return stateType != null;
        }
    }
}
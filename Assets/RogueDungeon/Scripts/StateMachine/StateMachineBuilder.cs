using System.Collections.Generic;
using UnityEngine.Assertions;

namespace RogueDungeon.StateMachine
{
    public interface IStateHandlersProvider
    {
        IEnumerable<IStateHandler> GetHandlers();
    }

    public class StateMachineBuilder
    {
        private readonly StatesContainer _statesContainer = new();
        private readonly TransitionsContainer _transitionsContainer = new();
        private readonly StateMachine _stateMachine;
        
        public StateMachineBuilder() => 
            _stateMachine = new StateMachine(_statesContainer, _transitionsContainer);
        
        public void AddState(IState state) => 
            _statesContainer.AddState(state);
        
        public void AddStateHandlers<T>(IStateHandlersProvider handlersProvider) where T : IState
        {
            var state = (StateWithHandlers)_statesContainer.GetState(typeof(T));
            
            foreach (var handler in handlersProvider.GetHandlers()) 
                state.AddAllHandlerInterfaces(handler);
        }

        public void AddTransitionToState<TTo>(ICondition condition) where TTo : IState=> 
            _transitionsContainer.Add(new Transition<TTo>(condition));
        
        public void AddTransitionFromToState<TFrom, TTo>(ICondition condition) where TFrom : IState where TTo : IState =>  
            AddTransitionToState<TTo>(new CompositeCondition(condition, new IsInStateCondition<TFrom>(_stateMachine)));
        
        public void AddTransitionFromFinishedState<TFrom, TTo>(ICondition condition = null) where TFrom : IState where TTo : IState
        {
            var finalCondition = condition != null
                ? new CompositeCondition(
                    condition,
                    new IsInStateCondition<TFrom>(_stateMachine),
                    new IsCurrentStateFinishedCondition(_stateMachine))
                : new CompositeCondition(
                    new IsInStateCondition<TFrom>(_stateMachine),
                    new IsCurrentStateFinishedCondition(_stateMachine));
                
            AddTransitionToState<TTo>(finalCondition);
        }

        public void SetStartState(IState state) => 
            _statesContainer.SetStartState(state);

        public StateMachine Build()
        {
            Assert.IsNotNull(_statesContainer.GetStartState());
            return _stateMachine;
        }
    }
}
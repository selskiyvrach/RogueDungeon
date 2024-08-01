using UnityEngine.Assertions;

namespace RogueDungeon.StateMachine
{
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
            _transitionsContainer.Add(new TransitionToStateOfType<TTo>(condition));
        
        public void AddTransitionToState(IState state, ICondition condition) => 
            _transitionsContainer.Add(new TransitionToState(state, condition));
        
        public void AddTransitionFromToState<TFrom, TTo>(ICondition condition) where TFrom : IState where TTo : IState =>  
            AddTransitionToState<TTo>(new IfAllCondition(condition, new IsInStateOfTypeCondition<TFrom>(_stateMachine)));
        
        public void AddTransitionFromToState(IState from, IState to, ICondition condition) =>  
            AddTransitionToState(to, new IfAllCondition(condition, new IsInStateCondition(_stateMachine, from)));
        
        public void AddTransitionFromFinishedState<TFrom, TTo>(ICondition condition = null) where TFrom : IState where TTo : IState
        {
            var finalCondition = condition != null
                ? new IfAllCondition(
                    condition,
                    new IsInStateOfTypeCondition<TFrom>(_stateMachine),
                    new IsCurrentStateFinishedCondition(_stateMachine))
                : new IfAllCondition(
                    new IsInStateOfTypeCondition<TFrom>(_stateMachine),
                    new IsCurrentStateFinishedCondition(_stateMachine));
                
            AddTransitionToState<TTo>(finalCondition);
        }
        
        public void AddTransitionFromFinishedState(IState from, IState to, ICondition condition = null)
        {
            var finalCondition = condition != null
                ? new IfAllCondition(
                    condition,
                    new IsInStateCondition(_stateMachine, from),
                    new IsCurrentStateFinishedCondition(_stateMachine))
                : new IfAllCondition(
                    new IsInStateCondition(_stateMachine, from),
                    new IsCurrentStateFinishedCondition(_stateMachine));
                
            AddTransitionToState(to, finalCondition);
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
using System;
using UnityEngine.Assertions;

namespace Common.FSM
{
    public class StateMachineBuilder : StateMachineBuilder<StateMachine>
    {
    }

    public class StateMachineBuilder<T> where T : StateMachine
    {
        private readonly StatesContainer _statesContainer = new();
        private readonly TransitionsContainer _transitionsContainer = new();
        private readonly T _stateMachine;
        
        public StateMachineBuilder() => 
            _stateMachine = (T)Activator.CreateInstance(typeof(T), _statesContainer, _transitionsContainer);
        
        public void AddState(IState state) => 
            _statesContainer.AddState(state);

        public void SetDebugName(string name) =>
            _stateMachine.DebugName = name;

       public void AddTransitionToState(IState state, ICondition condition) => 
            _transitionsContainer.Add(new TransitionToState(state, condition));
        
        public void AddTransition(IState from, IState to, ICondition condition) =>  
            AddTransitionToState(to, new IfAllCondition(condition, new IsInStateCondition(_stateMachine, from)));
        
        public void AddTransitionWhenFinished(IState from, IState to, IFinishable finishable, ICondition condition = null)
        {
            var finalCondition = condition != null
                ? new IfAllCondition(
                    condition,
                    new IsInStateCondition(_stateMachine, from),
                    new IsFinishedCondition(finishable))
                : new IfAllCondition(
                    new IsInStateCondition(_stateMachine, from),
                    new IsFinishedCondition(finishable));
                
            AddTransitionToState(to, finalCondition);
        }

        public void SetStartState(IState state) => 
            _statesContainer.SetStartState(state);

        public T Build()
        {
            Assert.IsNotNull(_statesContainer.GetStartState());
            return _stateMachine;
        }
    }
}
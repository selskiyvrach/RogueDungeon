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

        public void SetDebugName(string name) =>
            _stateMachine.DebugName = name;

       public void AddTransitionToState(IState state, ICondition condition) => 
            _transitionsContainer.Add(new TransitionToState(state, condition));
        
        public void AddTransitionFromToState(IState from, IState to, ICondition condition) =>  
            AddTransitionToState(to, new IfAllCondition(condition, new IsInStateCondition(_stateMachine, from)));
        
        public void AddTransitionFromFinishedState(IFinishableState from, IState to, ICondition condition = null)
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
using System;
using Common.Providers;
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

        public TState CreateState<TState>(string debugName = null) where TState : State, new()
        {
            var state = new TState();
            state.DebugName = debugName;
            _statesContainer.AddState(state);
            return state;
        }
        
        public CompetingState<TCompetition> CreateCompetingState<TCompetition>(TCompetition competition = default, string debugName = null) where TCompetition : IComparable<TCompetition>
        {
            var state = CreateState<CompetingState<TCompetition>>(debugName: debugName);
            state.Competition = competition;
            return state;
        }
        
        public CompetingState<EnumComparer<TEnum>> CreateEnumCompetingState<TEnum>(TEnum competition = default, string debugName = null) where TEnum : Enum => 
            CreateCompetingState(new EnumComparer<TEnum>(competition), debugName);

        public IState CreateState(string debugName = null) => 
            CreateState<State>(debugName);

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

        // use this decorator to set up transitions depending on current state before building the state machine itself
        public T Build(IProviderDecorator<IState> stateProviderDecorator = null)
        {
            Assert.IsNotNull(_statesContainer.GetStartState());
            if (stateProviderDecorator != null)
                stateProviderDecorator.DecoratedProvider = _stateMachine;
            return _stateMachine;
        }
    }
}
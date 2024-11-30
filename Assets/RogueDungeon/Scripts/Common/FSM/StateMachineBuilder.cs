using System;
using RogueDungeon.Entities.Properties;
using UnityEngine.Assertions;

namespace Common.FSM
{
    public class StateMachineBuilder
    {
        private readonly StatesContainer _statesContainer = new();
        private readonly TransitionsContainer _transitionsContainer = new();
        private readonly StateMachine _stateMachine;
        
        public StateMachineBuilder(params IState[] states)
        {
            _stateMachine = new StateMachine(_statesContainer, _transitionsContainer);
            for (var i = 0; i < states.Length; i++)
            {
                if(i == 0)
                    _statesContainer.AddStartState(states[0]);
                else
                    _statesContainer.Add(states[i]);
            }
        }

        public T AddState<T>(T state) where T : IState
        {
            _statesContainer.Add(state);
            return state;
        }

        public TState CreateState<TState>(string debugName = null) where TState : State, new()
        {
            var state = new TState();
            state.DebugName = debugName;
            _statesContainer.Add(state);
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

        public void SetDebugName(string name) =>
            _stateMachine.DebugName = name;

       public void AddTransitionToState(IState state, ICondition condition) => 
            _transitionsContainer.Add(new TransitionToState(state, condition));
        
        public void AddTransition(IState from, IState to, ICondition condition) =>  
            AddTransitionToState(to, new IfAll(condition, new IsInStateCondition(_stateMachine, from)));
        
        public void AddTransitionFromFinished(IState from, IState to, IFinishable finishable, ICondition condition = null)
        {
            var finalCondition = condition != null
                ? new IfAll(
                    condition,
                    new IsInStateCondition(_stateMachine, from),
                    new IsFinishedCondition(finishable))
                : new IfAll(
                    new IsInStateCondition(_stateMachine, from),
                    new IsFinishedCondition(finishable));
                
            AddTransitionToState(to, finalCondition);
        }
        
        public void AddTransitionFromFinished(IFinishableState from, IState to, ICondition condition = null) => 
            AddTransitionFromFinished(from, to, from, condition);

        public void SetStartState(IState state) => 
            _statesContainer.AddStartState(state);

        // use this decorator to set up transitions depending on current state before building the state machine itself
        public StateMachine Build(IReadOnlyPropertyDecorator<IState> stateProviderDecorator = null)
        {
            Assert.IsNotNull(_statesContainer.GetStartState());
            if (stateProviderDecorator != null)
                stateProviderDecorator.Decorated = _stateMachine;
            return _stateMachine;
        }
    }
}
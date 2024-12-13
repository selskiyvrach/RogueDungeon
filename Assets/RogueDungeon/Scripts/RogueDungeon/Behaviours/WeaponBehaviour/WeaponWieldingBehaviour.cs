using System;
using System.Collections.Generic;
using Common.Behaviours;
using Common.DotNetUtils;

namespace RogueDungeon.Behaviours.WeaponBehaviour
{
    internal class WeaponWieldingBehaviour : Behaviour, IStateChanger
    {
        private readonly IStatesFactory _statesFactory;
        private readonly HashSet<IState> _transitionsHistory = new();
        private IState _currentState;

        public WeaponWieldingBehaviour(IStatesFactory statesFactory) => 
            _statesFactory = statesFactory;

        public override void Enable()
        {
            base.Enable();
            To<IdleState>();
        }

        public override void Tick(float timeDelta)
        {
            _currentState.Tick(timeDelta);
            _currentState.CheckTransitions(this);
        }

        public void To<T>() where T : IState
        {
            _transitionsHistory.Clear();
            _currentState.Exit();
            _currentState = _statesFactory.Create<T>();
            if (!_transitionsHistory.Add(_currentState))
                throw new InvalidOperationException("Infinite transitions loop detected: " + _transitionsHistory.JoinTypeNames());

            _currentState.Enter();
            _currentState.CheckTransitions(this);
        }
    }
}
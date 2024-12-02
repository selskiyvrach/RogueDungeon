using System;
using Common.GameObjectMarkers;
using Common.Properties;
using Common.ZenjectUtils;
using RogueDungeon.Collisions;
using RogueDungeon.Entities;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Player
{
    public class Player : IGameEntity, IDodger
    {
        private readonly PlayerRootObject _playerRoot;
        private IDisposable _sub;

        public Transform RootTransform => _playerRoot.transform;
        public Positions Position => _dodgeState.Value.ToPlayerPosition();
        public DodgeState DodgeState => _dodgeState.Value;

        private readonly IProperty<AttackState> _attackState;
        private readonly IProperty<DodgeState> _dodgeState;

        [Inject] private DodgeBehaviour _dodgeBehaviour;
        [Inject] private AttackBehaviour _attackBehaviour;

        public Player(IProperty<AttackState> attackState, IProperty<DodgeState> dodgeState, PlayerRootObject playerRoot)
        {
            _attackState = attackState;
            _dodgeState = dodgeState;
            _playerRoot = playerRoot;
        }
    }
}
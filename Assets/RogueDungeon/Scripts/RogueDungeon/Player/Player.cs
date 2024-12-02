using System;
using Common.GameObjectMarkers;
using Common.Properties;
using Common.ZenjectUtils;
using RogueDungeon.Collisions;
using RogueDungeon.Entities;
using UnityEngine;

namespace RogueDungeon.Player
{
    [CreateFactoryInstaller]
    public class Player : IGameEntity, IDodger
    {
        private readonly PlayerRootObject _playerRoot;
        private IDisposable _sub;

        public Transform RootTransform => _playerRoot.transform;
        public Positions Position => _dodgeState.Value.ToPlayerPosition();
        public DodgeState DodgeState => _dodgeState.Value;

        private readonly Property<AttackState> _attackState;
        private readonly Property<DodgeState> _dodgeState;

        public Player(Property<AttackState> attackState, Property<DodgeState> dodgeState, PlayerRootObject playerRoot)
        {
            _attackState = attackState;
            _dodgeState = dodgeState;
            _playerRoot = playerRoot;
        }

        public void Enable()
        {
        }

        public void Disable()
        {
        }
    }
}
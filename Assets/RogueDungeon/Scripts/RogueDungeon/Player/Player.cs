using System;
using Common.GameObjectMarkers;
using Common.Properties;
using RogueDungeon.Behaviours;
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

        private readonly IProperty<DodgeState> _dodgeState;

        [Inject] private DodgeBehaviour _dodgeBehaviour;
        [Inject] private AttackBehaviour _attackBehaviour;

        public Player(IProperty<DodgeState> dodgeState, PlayerRootObject playerRoot, AttackBehaviour attackBehaviour, DodgeBehaviour dodgeBehaviour)
        {
            _dodgeState = dodgeState;
            _playerRoot = playerRoot;
            attackBehaviour.Enable();
            dodgeBehaviour.Enable();
        }
    }
}
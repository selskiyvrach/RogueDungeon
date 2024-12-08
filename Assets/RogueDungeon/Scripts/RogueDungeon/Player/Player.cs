using System;
using Common.GameObjectMarkers;
using Common.Properties;
using RogueDungeon.Behaviours.MovementBehaviour;
using RogueDungeon.Collisions;
using RogueDungeon.Entities;
using RogueDungeon.Game;
using RogueDungeon.Weapons;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Player
{
    public class Player : IGameEntity
    {
        private readonly PlayerRootObject _playerRoot;
        private IDisposable _sub;

        public Transform RootTransform => _playerRoot.transform;
        public Positions Position => _dodgeState.Value.ToPlayerPosition();
        public DodgeState DodgeState => _dodgeState.Value;

        private readonly IProperty<DodgeState> _dodgeState;

        [Inject] private MovementBehaviour _movementBehaviour;
        [Inject] private WeaponBehaviour _weaponBehaviour;

        public Player(IProperty<DodgeState> dodgeState, PlayerRootObject playerRoot, WeaponBehaviour weaponBehaviour, MovementBehaviour movementBehaviour)
        {
            _dodgeState = dodgeState;
            _playerRoot = playerRoot;
            weaponBehaviour.Enable();
            movementBehaviour.Enable();
        }
    }
}
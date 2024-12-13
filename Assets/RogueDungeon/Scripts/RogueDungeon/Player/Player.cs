using System;
using Common.GameObjectMarkers;
using Common.Properties;
using RogueDungeon.Behaviours.MovementBehaviour;
using RogueDungeon.Collisions;
using RogueDungeon.Entities;
using RogueDungeon.Game;
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

        private readonly IProperty<DodgeState> _dodgeState;

        public Player(IProperty<DodgeState> dodgeState, PlayerRootObject playerRoot)
        {
            _dodgeState = dodgeState;
            _playerRoot = playerRoot;
        }
    }
}
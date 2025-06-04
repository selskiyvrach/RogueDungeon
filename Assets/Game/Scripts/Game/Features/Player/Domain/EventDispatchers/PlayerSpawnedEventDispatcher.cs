using System;
using UnityEngine;

namespace Game.Features.Player.Domain.EventDispatchers
{
    public class PlayerSpawnedEventDispatcher
    {
        private readonly PlayerSpawner _playerSpawner;

        public event Action OnPlayerSpawned
        {
            add => _playerSpawner.OnPlayerSpawned += value;
            remove => _playerSpawner.OnPlayerSpawned -= value;
        }

        public event Action OnPlayerDespawned
        {
            add => _playerSpawner.OnPlayerDespawned += value;
            remove => _playerSpawner.OnPlayerDespawned -= value;
        }

        public PlayerSpawnedEventDispatcher(PlayerSpawner playerSpawner) => 
            _playerSpawner = playerSpawner;
    }

    public class PlayerCameraReferencePointPositionChangedEventDispatcher
    {
        // private readonly 
        
        public event Action<Vector3> OnPointPositionChanged;
    }
}
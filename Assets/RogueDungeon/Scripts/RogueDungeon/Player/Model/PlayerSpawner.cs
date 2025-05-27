using UnityEngine;
using Zenject;

namespace Player.Model
{
    public class PlayerSpawner : IPlayerSpawner
    {
        private readonly IPlayerRegistry _playerRegistry;
        private readonly IFactory<PlayerConfig, Transform, Player> _factory;
        private readonly Transform _playerParent;
        private readonly PlayerConfig _config;

        public PlayerSpawner(IPlayerRegistry playerRegistry, IFactory<PlayerConfig, Transform, Player> factory, Transform playerParent, PlayerConfig config)
        {
            _playerRegistry = playerRegistry;
            _factory = factory;
            _playerParent = playerParent;
            _config = config;
        }

        public Player Spawn()
        {
            var player = _factory.Create(_config, _playerParent);
            
            _playerRegistry.Player = player;
            return player;
        }
    }
}
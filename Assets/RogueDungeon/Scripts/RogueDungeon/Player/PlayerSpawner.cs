using RogueDungeon.Combat;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Player
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

        public void Spawn() => 
            _playerRegistry.RegisterPlayer(_factory.Create(_config, _playerParent));
    }
}
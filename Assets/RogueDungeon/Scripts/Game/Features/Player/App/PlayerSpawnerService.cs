using Game.Features.Player.Domain;
using Game.Libs.Time;
using ModestTree;
using UnityEngine;
using Zenject;
using ITickable = Libs.Lifecycle.ITickable;

namespace Game.Features.Player.App
{
    public class PlayerSpawner : IPlayerSpawnerService, IPlayerDespawnerService
    {
        private readonly IFactory<ITickable, TickOrder, ObjectTicker> _tickerFactory;
        private readonly IFactory<PlayerConfig, Transform, PlayerModel> _factory;
        private readonly PlayerConfig _config;
        private readonly Transform _playerParent;
        private PlayerModel _player;

        public PlayerSpawner(IFactory<PlayerConfig, Transform, PlayerModel> factory, Transform playerParent, PlayerConfig config, 
            IFactory<ITickable, TickOrder, ObjectTicker> tickerFactory)
        {
            _factory = factory;
            _playerParent = playerParent;
            _config = config;
            _tickerFactory = tickerFactory;
        }

        public void SpawnPlayer()
        {
            Assert.IsNull(_player, "Player instance is already spawned.");
            _player = _factory.Create(_config, _playerParent);
            _player.Initialize();
            _ticker = _tickerFactory.Create(_player, TickOrder.Player);
            _ticker.Initialize();
        }

        public void DespawnPlayer()
        {
            _ticker.Dispose();
            _player.Dispose();
            _player = null;
            _ticker = null;
        }
    }
}
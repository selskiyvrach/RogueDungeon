using System;
using Game.Features.Combat.Domain.Enemies;
using Game.Features.Combat.Domain.Enemies.HiveMind;
using Libs.Lifecycle;
using UnityEngine;

namespace Game.Features.Combat.Domain
{
    public class Combat : ITickable, ICombat
    {
        private readonly IBattleFieldFactory _battleFieldFactory;
        private readonly ICombatConfigsRepository _configsRepository;
        private readonly IEnemySpawner _enemySpawner;
        private readonly HiveMind _hiveMind;
        private bool _isRunning;

        public event Action OnFinished;
        public event Action OnStarted;

        public Vector2Int Coordinates { get; private set; }
        public string Id { get; private set; }

        public Combat(ICombatConfigsRepository configsRepository, IEnemySpawner enemySpawner, IBattleFieldFactory battleFieldFactory, HiveMind hiveMind)
        {
            _configsRepository = configsRepository;
            _enemySpawner = enemySpawner;
            _battleFieldFactory = battleFieldFactory;
            _hiveMind = hiveMind;
        }

        public void Initiate(string id, Vector2Int position, Vector2Int rotation)
        {
            Id = id;
            Coordinates = position;
            var battleField = _battleFieldFactory.CreateBattleField(position, rotation);

            foreach (var spawnInfo in _configsRepository.Get(id).SpawnInfos)
                _hiveMind.Add(_enemySpawner.Spawn(spawnInfo.config.Id, spawnInfo.position, battleField));
            
            _isRunning = true;
            OnStarted?.Invoke();
        }

        public void Tick(float timeDelta)
        {
            if(!_isRunning)
                return;
            
            if (_hiveMind.Enemies.Count > 0)
            {
                _hiveMind.Tick(timeDelta);
                return;
            }
            _isRunning = false;
            OnFinished?.Invoke();
        }
    }
}
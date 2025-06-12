using Game.Features.Combat.Domain.Enemies;
using Libs.Utils.DotNet;
using UnityEngine;

namespace Game.Features.Combat.Domain
{
    public class Combat
    {
        private readonly IBattleFieldFactory _battleFieldFactory;
        private readonly ICombatConfigsRepository _configsRepository;
        private readonly IEnemySpawner _enemySpawner;

        public Combat(ICombatConfigsRepository configsRepository, IEnemySpawner enemySpawner, IBattleFieldFactory battleFieldFactory)
        {
            _configsRepository = configsRepository;
            _enemySpawner = enemySpawner;
            _battleFieldFactory = battleFieldFactory;
        }

        public void Initiate(string id, Vector2Int position, Vector2Int rotation)
        {
            if(id.IsNullOrEmpty())
                return;
            
            var battleField = _battleFieldFactory.CreateBattleField(position, rotation);
            
            foreach (var spawnInfo in _configsRepository.Get(id).SpawnInfos) 
                _enemySpawner.Spawn(spawnInfo.config.Id, spawnInfo.position, battleField);
        }
    }
}
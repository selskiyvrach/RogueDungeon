using System.Collections;
using System.Linq;
using Common.Unity;
using RogueDungeon.Combat;
using RogueDungeon.Enemies;
using RogueDungeon.Game.Gameplay;
using UnityEngine;

namespace RogueDungeon.Levels
{
    public class CombatRoomEvent : RoomEvent
    {
        private readonly IGameplayModeChanger _gameplayModeChanger;
        private readonly BattleField _battleField;
        private readonly CombatRoomEventConfig _config;
        private readonly IEnemiesRegistry _enemiesRegistry;
        private readonly IEnemySpawner _enemySpawner;
        private readonly Level _level;

        public override RoomEventPriority Priority => RoomEventPriority.Combat;

        public CombatRoomEvent(IEnemiesRegistry enemiesRegistry, CombatRoomEventConfig config, IEnemySpawner enemySpawner, BattleField battleField, Level level, IGameplayModeChanger gameplayModeChanger)
        {
            _enemiesRegistry = enemiesRegistry;
            _config = config;
            _enemySpawner = enemySpawner;
            _battleField = battleField;
            _level = level;
            _gameplayModeChanger = gameplayModeChanger;
        }

        public override IEnumerator ProcessEvent(Room room)
        {
            _gameplayModeChanger.SetCombatMode();
            
            yield return base.ProcessEvent(room);
            
            _battleField.Position = room.Coordinates;
            _battleField.Direction = _level.LevelTraverser.Direction.Round();
            
            if(_config.MiddleEnemy is {} middleEnemy)
                _enemySpawner.Spawn(middleEnemy, EnemyPosition.Middle);
            if(_config.RightEnemy is {} rightEnemy)
                _enemySpawner.Spawn(rightEnemy, EnemyPosition.Right);
            if(_config.LeftEnemy is {} leftEnemy)
                _enemySpawner.Spawn(leftEnemy, EnemyPosition.Left);
            
            foreach (var enemy in _enemiesRegistry.Enemies) 
                enemy.Enable();
            
            yield return new WaitUntil(() => !_enemiesRegistry.Enemies.Any());
            _gameplayModeChanger.SetExplorationMode();
        }
    }
}
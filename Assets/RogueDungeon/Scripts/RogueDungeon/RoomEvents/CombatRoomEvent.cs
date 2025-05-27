using Combat;
using Common.UtilsUnity;
using Enemies;
using Enemies.HiveMind;
using Levels;

namespace RoomEvents
{
    public class CombatRoomEvent : RoomEvent
    {
        private readonly BattleField _battleField;
        private readonly CombatRoomEventConfig _config;
        private readonly IEnemiesRegistry _enemiesRegistry;
        private readonly IEnemySpawner _enemySpawner;
        private readonly Level _level;
        private readonly HiveMind _hiveMind;

        public override RoomEventPriority Priority => RoomEventPriority.Combat;
        public override bool IsFinished => _enemiesRegistry.Enemies.Count == 0;

        public CombatRoomEvent(IEnemiesRegistry enemiesRegistry, CombatRoomEventConfig config, IEnemySpawner enemySpawner, BattleField battleField, Level level, HiveMind hiveMind)
        {
            _enemiesRegistry = enemiesRegistry;
            _config = config;
            _enemySpawner = enemySpawner;
            _battleField = battleField;
            _level = level;
            _hiveMind = hiveMind;
        }

        public override void Tick(float timeDelta)
        {
            base.Tick(timeDelta);
            _hiveMind.Tick(timeDelta);
        }

        public override void Enter()
        {
            base.Enter();
            
            _battleField.Position = Room.Coordinates;
            _battleField.Direction = _level.LevelTraverser.Rotation2D.Round();
            
            if(_config.MiddleEnemy is {} middleEnemy)
                _enemySpawner.Spawn(middleEnemy, EnemyPosition.Middle);
            if(_config.RightEnemy is {} rightEnemy)
                _enemySpawner.Spawn(rightEnemy, EnemyPosition.Right);
            if(_config.LeftEnemy is {} leftEnemy)
                _enemySpawner.Spawn(leftEnemy, EnemyPosition.Left);
            
            foreach (var enemy in _enemiesRegistry.Enemies) 
                enemy.Initialize();
        }
    }
}
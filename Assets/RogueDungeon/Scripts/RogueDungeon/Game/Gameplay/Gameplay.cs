using RogueDungeon.Camera;
using RogueDungeon.Combat;
using RogueDungeon.Enemies;
using RogueDungeon.Levels;
using RogueDungeon.Player;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Game.Gameplay
{
    public class Gameplay : Common.Behaviours.Behaviour
    {
        private readonly ICombatantsRegistry _combatantsRegistry;
        private readonly GameplayConfig _config;
        private readonly IPlayerSpawner _playerSpawner;
        private readonly IEnemySpawner _enemySpawner;
        private readonly IGameCamera _camera;
        private readonly IFactory<LevelConfig, Level> _levelFactory;

        public Gameplay(GameplayConfig config, IPlayerSpawner playerSpawner, ICombatantsRegistry combatantsRegistry, IEnemySpawner enemySpawner, IGameCamera camera, IFactory<LevelConfig, Level> levelFactory)
        {
            _config = config;
            _playerSpawner = playerSpawner;
            _combatantsRegistry = combatantsRegistry;
            _enemySpawner = enemySpawner;
            _camera = camera;
            _levelFactory = levelFactory;
        }

        public override void Enable()
        {
            base.Enable();
            _levelFactory.Create(_config.LevelConfig);
            var player = _playerSpawner.Spawn();
            _camera.Follow = player.GameObject.CameraReferencePoint;
            player.Enable();
            _enemySpawner.Spawn(new EnemySpawningArgs(_config.TestEnemy, EnemyPosition.Left));
            _enemySpawner.Spawn(new EnemySpawningArgs(_config.TestEnemy, EnemyPosition.Middle));
        }
    }
}
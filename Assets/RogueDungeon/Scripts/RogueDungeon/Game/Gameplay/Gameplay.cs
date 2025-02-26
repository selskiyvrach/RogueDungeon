using Common.Behaviours;
using RogueDungeon.Camera;
using RogueDungeon.Combat;
using RogueDungeon.Enemies;
using RogueDungeon.Input;
using RogueDungeon.Levels;
using RogueDungeon.Player;
using Zenject;

namespace RogueDungeon.Game.Gameplay
{
    public class Gameplay : Behaviour, IGameplayModeChanger
    {
        private readonly IPlayerInput _playerInput;
        private readonly ICombatantsRegistry _combatantsRegistry;
        private readonly GameplayConfig _config;
        private readonly IPlayerSpawner _playerSpawner;
        private readonly IEnemySpawner _enemySpawner;
        private readonly IGameCamera _camera;
        private readonly IFactory<LevelConfig, Level> _levelFactory;

        public Gameplay(GameplayConfig config, IPlayerSpawner playerSpawner, ICombatantsRegistry combatantsRegistry, IEnemySpawner enemySpawner, IGameCamera camera, IFactory<LevelConfig, Level> levelFactory, IPlayerInput playerInput)
        {
            _config = config;
            _playerSpawner = playerSpawner;
            _combatantsRegistry = combatantsRegistry;
            _enemySpawner = enemySpawner;
            _camera = camera;
            _levelFactory = levelFactory;
            _playerInput = playerInput;
        }

        public override void Enable()
        {
            base.Enable();
            var level = _levelFactory.Create(_config.LevelConfig);
            var player = _playerSpawner.Spawn();
            level.LevelTraverser = player;
            level.Initialize();
            _camera.Follow = player.GameObject.CameraReferencePoint;
            player.Enable();
            _playerInput.Enable();
        }

        public void SetExplorationMode() => 
            _playerInput.SetFilter(_config.ExplorationInputFilter);

        public void SetCombatMode() => 
            _playerInput.SetFilter(_config.CombatInputFilter);
    }
}
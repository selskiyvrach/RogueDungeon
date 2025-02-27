using Common.Behaviours;
using RogueDungeon.Camera;
using RogueDungeon.Combat;
using RogueDungeon.Enemies;
using RogueDungeon.Enemies.HiveMind;
using RogueDungeon.Input;
using RogueDungeon.Levels;
using RogueDungeon.Player;
using Zenject;

namespace RogueDungeon.Game.Gameplay
{
    public class Gameplay : Behaviour, IGameplayModeChanger
    {
        private readonly IPlayerInput _playerInput;
        private readonly GameplayConfig _config;
        private readonly IPlayerSpawner _playerSpawner;
        private readonly IGameCamera _camera;
        private readonly IFactory<LevelConfig, Level> _levelFactory;
        private readonly HiveMindBehaviour _hiveMind;

        public Gameplay(GameplayConfig config, IPlayerSpawner playerSpawner, IGameCamera camera, IFactory<LevelConfig, Level> levelFactory, IPlayerInput playerInput, HiveMindBehaviour hiveMind)
        {
            _config = config;
            _playerSpawner = playerSpawner;
            _camera = camera;
            _levelFactory = levelFactory;
            _playerInput = playerInput;
            _hiveMind = hiveMind;
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

        public void SetExplorationMode()
        {
            _playerInput.SetFilter(_config.ExplorationInputFilter);
            _hiveMind.Disable();
        }

        public void SetCombatMode()
        {
            _playerInput.SetFilter(_config.CombatInputFilter);
            _hiveMind.Enable();
        }
    }
}
using RogueDungeon.Camera;
using RogueDungeon.Enemies.HiveMind;
using RogueDungeon.Input;
using RogueDungeon.Levels;
using RogueDungeon.Player;
using Zenject;
using IInitializable = Common.Lifecycle.IInitializable;
using ITickable = Common.Lifecycle.ITickable;

namespace RogueDungeon.Game.Gameplay
{
    public class Gameplay : IInitializable, IGameplayModeChanger, ITickable
    {
        private readonly IPlayerInput _playerInput;
        private readonly GameplayConfig _config;
        private readonly IPlayerSpawner _playerSpawner;
        private readonly IGameCamera _camera;
        private readonly IFactory<LevelConfig, Level> _levelFactory;
        private readonly HiveMind _hiveMind;
        
        private Player.Player _player;

        public Gameplay(GameplayConfig config, IPlayerSpawner playerSpawner, IGameCamera camera, IFactory<LevelConfig, Level> levelFactory, IPlayerInput playerInput, HiveMind hiveMind)
        {
            _config = config;
            _playerSpawner = playerSpawner;
            _camera = camera;
            _levelFactory = levelFactory;
            _playerInput = playerInput;
            _hiveMind = hiveMind;
        }

        public void Initialize()
        {
            var level = _levelFactory.Create(_config.LevelConfig);
            _player = _playerSpawner.Spawn();
            level.LevelTraverser = _player.WorldObject;
            level.Initialize();
            _camera.Follow = _player.CameraPovPoint;
            _player.Initialize();
            _hiveMind.Initialize();
        }

        public void Tick(float timeDelta)
        {
            _playerInput.Tick(timeDelta);
            _player.Tick(timeDelta);
            _hiveMind.Tick(timeDelta);
        }

        public void SetExplorationMode()
        {
            _playerInput.SetFilter(_config.ExplorationInputFilter);
        }

        public void SetCombatMode()
        {
            _playerInput.SetFilter(_config.CombatInputFilter);
        }
    }
}
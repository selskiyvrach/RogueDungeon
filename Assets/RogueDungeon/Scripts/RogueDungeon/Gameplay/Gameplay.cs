using RogueDungeon.Camera;
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
        private readonly IFactory<GameplayHud> _hudFactory;
        
        private Player.Player _player;
        private Level _level;

        public Gameplay(GameplayConfig config, IPlayerSpawner playerSpawner, IGameCamera camera, IFactory<LevelConfig, Level> levelFactory, IPlayerInput playerInput, IFactory<GameplayHud> hudFactory)
        {
            _config = config;
            _playerSpawner = playerSpawner;
            _camera = camera;
            _levelFactory = levelFactory;
            _playerInput = playerInput;
            _hudFactory = hudFactory;
        }

        public void Initialize()
        {
            _level = _levelFactory.Create(_config.LevelConfig);
            _player = _playerSpawner.Spawn();
            _level.Initialize();
            _camera.Follow = _player.CameraPovPoint;
            _player.Initialize();
            _hudFactory.Create();
        }

        public void Tick(float timeDelta)
        {
            _playerInput.Tick(timeDelta);
            _player.Tick(timeDelta);
            _level.Tick(timeDelta);
            if (!_player.IsAlive) 
                HandleGameOver();
        }

        private void HandleGameOver()
        {
            throw new System.NotImplementedException();
        }

        public void SetExplorationMode() => 
            _playerInput.SetFilter(_config.ExplorationInputFilter);

        public void SetCombatMode() => 
            _playerInput.SetFilter(_config.CombatInputFilter);
    }
}
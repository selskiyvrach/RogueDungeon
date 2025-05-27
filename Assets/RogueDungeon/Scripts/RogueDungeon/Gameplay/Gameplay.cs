using Camera;
using Input;
using Levels;
using Player.Model;
using UnityEngine.SceneManagement;
using Zenject;
using IInitializable = Common.Lifecycle.IInitializable;
using ITickable = Common.Lifecycle.ITickable;

namespace Gameplay
{
    public class Gameplay : IInitializable, ITickable
    {
        private readonly IPlayerInput _playerInput;
        private readonly GameplayUiManager _uiManager;
        private readonly GameplayConfig _config;
        private readonly IPlayerSpawner _playerSpawner;
        private readonly IGameCamera _camera;
        private readonly IFactory<LevelConfig, Level> _levelFactory;

        private Player.Model.Player _player;
        private Level _level;
        private bool _playerHasDied;
        
        public Gameplay(GameplayConfig config, IPlayerSpawner playerSpawner, IGameCamera camera, IFactory<LevelConfig, Level> levelFactory, IPlayerInput playerInput, GameplayUiManager uiManager)
        {
            _config = config;
            _playerSpawner = playerSpawner;
            _camera = camera;
            _levelFactory = levelFactory;
            _playerInput = playerInput;
            _uiManager = uiManager;
        }

        public void Initialize()
        {
            _level = _levelFactory.Create(_config.LevelConfig);
            _player = _playerSpawner.Spawn();
            _level.Initialize();
            _camera.Follow = _player.CameraPovPoint;
            _player.Initialize();
            _uiManager.Initialize();
        }

        public void Tick(float timeDelta)
        {
            _playerInput.Tick(timeDelta);
            _player.Tick(timeDelta);
            _level.Tick(timeDelta);
            
            if (!_player.IsReadyToBeDisposed || _playerHasDied) 
                return;
            _playerHasDied = true;
            _uiManager.Show<GameOverScreen>();
        }
        
        public void Restart() => 
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
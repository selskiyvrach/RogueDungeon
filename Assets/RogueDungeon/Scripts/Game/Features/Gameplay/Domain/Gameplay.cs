using System;
using Game.Features.Levels.Domain;
using Game.Features.Player.App;
using Game.Features.Player.Domain;
using Game.Libs.Camera;
using Game.Libs.Input;
using UnityEngine.SceneManagement;
using Zenject;
using IInitializable = Libs.Lifecycle.IInitializable;
using ITickable = Libs.Lifecycle.ITickable;

namespace Game.Features.Gameplay.Domain
{
    public class Gameplay : IInitializable, ITickable
    {
        private readonly IPlayerInput _playerInput;
        private readonly GameplayUiManager _uiManager;
        private readonly GameplayConfig _config;
        private readonly IPlayerSpawnerService _playerSpawner;
        private readonly IPlayerDespawnerService _playerDespawner;
        private readonly IPlayerDeathEventDispatcher _playerDeathEventDispatcher;
        private readonly IGameCamera _camera;
        private readonly IFactory<LevelConfig, Level> _levelFactory;

        private PlayerModel _player;
        private Level _level;
        private bool _playerHasDied;
        
        public Gameplay(GameplayConfig config, IPlayerSpawnerService playerSpawner, IGameCamera camera, IFactory<LevelConfig, Level> levelFactory, IPlayerInput playerInput, GameplayUiManager uiManager, IPlayerDespawnerService playerDespawner, IPlayerDeathEventDispatcher playerDeathEventDispatcher)
        {
            _config = config;
            _playerSpawner = playerSpawner;
            _camera = camera;
            _levelFactory = levelFactory;
            _playerInput = playerInput;
            _uiManager = uiManager;
            _playerDespawner = playerDespawner;
            _playerDeathEventDispatcher = playerDeathEventDispatcher;
        }

        public void Initialize()
        {
            _playerDeathEventDispatcher.OnPlayerDeathStageReached += HandleDeathStage;
            _level = _levelFactory.Create(_config.LevelConfig);
            _playerSpawner.SpawnPlayer();
            _level.Initialize();
            _camera.Follow = _player.CameraPovPoint;
            _player.Initialize();
            _uiManager.Initialize();
        }

        private void HandleDeathStage(PlayerDeathStage stage)
        {
            switch (stage)
            {
                case PlayerDeathStage.JustReachedZeroHealth:
                    break;
                case PlayerDeathStage.FinishedPlayingDeathAnimation:
                    _playerDespawner.DespawnPlayer();
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(stage), stage, null);
            }
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
    }
}
using Game.Features.Player.Domain;

namespace Game.Features.Player.App.UseCases.Feature
{
    public class SpawnPlayerOnGameplayStartedUseCase
    {
        private readonly PlayerSpawner _playerSpawner;
        private readonly global::Game.Features.Gameplay.Domain.Gameplay _gameplay;

        public SpawnPlayerOnGameplayStartedUseCase(global::Game.Features.Gameplay.Domain.Gameplay gameplay, PlayerSpawner playerSpawner)
        {
            _gameplay = gameplay;
            _playerSpawner = playerSpawner;
            _gameplay.OnStartGameplayRequested += _playerSpawner.SpawnPlayer;
        }
    }
}
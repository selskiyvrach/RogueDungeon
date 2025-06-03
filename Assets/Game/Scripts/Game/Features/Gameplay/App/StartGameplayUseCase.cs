using Zenject;

namespace Game.Features.Gameplay.App
{
    public class StartGameplayUseCase : IInitializable
    {
        private readonly Domain.Gameplay _gameplay;

        public StartGameplayUseCase(Domain.Gameplay gameplay) => 
            _gameplay = gameplay;

        public void Initialize() => 
            _gameplay.Initialize();
    }
}
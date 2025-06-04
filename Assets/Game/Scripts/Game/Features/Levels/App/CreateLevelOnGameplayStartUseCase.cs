using Game.Features.Levels.Domain;

namespace Game.Features.Levels.App
{
    public class CreateLevelOnGameplayStartUseCase
    {
        private readonly ILevelFactory _factory;
        private readonly ILevelConfig _config;

        public CreateLevelOnGameplayStartUseCase(Gameplay.Domain.Gameplay gameplay, ILevelFactory levelFactory, ILevelConfig config)
        {
            _factory = levelFactory;
            _config = config;
            gameplay.OnPrepareGameplayElementsRequested += CreateLevel;
        }

        private void CreateLevel() => 
            _factory.Create(_config);
    }
}
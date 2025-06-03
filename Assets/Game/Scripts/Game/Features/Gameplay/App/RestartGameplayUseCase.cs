namespace Game.Features.Gameplay.App
{
    public class RestartGameplayOnPlayerDeathUseCase
    {
        private readonly Domain.Gameplay _gameplay;

        public RestartGameplayOnPlayerDeathUseCase(Domain.Gameplay gameplay)
        {
            _gameplay = gameplay;
        }
        
        
    }
}
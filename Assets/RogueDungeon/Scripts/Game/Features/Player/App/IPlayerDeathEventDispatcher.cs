using System;

namespace Game.Features.Player.App
{
    public interface IPlayerDeathEventDispatcher
    {
        event Action<PlayerDeathStage> OnPlayerDeathStageReached;
    }

    public enum PlayerDeathStage
    {
        None,
        JustReachedZeroHealth,
        FinishedPlayingDeathAnimation,
    }
}
using System;

namespace Game.Features.Player.Domain
{
    public interface IPlayerDespawnedEventDispatcher
    {
        event Action<Player> OnPlayerDespawned;
    }
}
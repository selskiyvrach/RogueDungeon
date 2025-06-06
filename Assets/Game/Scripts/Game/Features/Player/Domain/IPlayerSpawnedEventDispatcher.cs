using System;

namespace Game.Features.Player.Domain
{
    public interface IPlayerSpawnedEventDispatcher
    {
        event Action<Player> OnPlayerSpawned;
    }
}
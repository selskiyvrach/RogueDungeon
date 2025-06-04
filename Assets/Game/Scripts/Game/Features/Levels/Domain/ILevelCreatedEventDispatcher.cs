using System;

namespace Game.Features.Levels.Domain
{
    public interface ILevelCreatedEventDispatcher
    {
        event Action<Level> OnLevelCreated;
    }
}
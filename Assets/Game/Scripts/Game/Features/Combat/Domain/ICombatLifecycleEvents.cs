using System;

namespace Game.Features.Combat.Domain
{
    public interface ICombatLifecycleEvents
    {
        event Action OnFinished;
        event Action OnStarted;
    }
}
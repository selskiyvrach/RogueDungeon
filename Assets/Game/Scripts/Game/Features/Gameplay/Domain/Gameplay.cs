using System;
using Libs.Lifecycle;

namespace Game.Features.Gameplay.Domain
{
    public class Gameplay : IInitializable
    {
        public event Action OnGameplayStarted;

        public void Initialize()
        {
            OnGameplayStarted?.Invoke();
        }
        
        
    }
}
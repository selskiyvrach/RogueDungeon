using System;
using Libs.Lifecycle;

namespace Game.Features.Gameplay.Domain
{
    public class Gameplay : IInitializable
    {
        public event Action OnPrepareGameplayElementsRequested;
        public event Action OnStartGameplayRequested;
        public event Action OnGameStarted;

        public void Initialize()
        {
            OnPrepareGameplayElementsRequested?.Invoke();
            OnStartGameplayRequested?.Invoke();
            OnGameStarted?.Invoke();
        }
    }
}
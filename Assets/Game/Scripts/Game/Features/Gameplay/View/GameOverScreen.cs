using System;
using Game.UI.Screens;

namespace Game.Features.Gameplay.View
{
    public class GameOverScreen : Screen
    {
        protected override DrawOrder DrawOrder => DrawOrder.GameOver;
        public event Action OnRestartPressed;

        private void Update()
        {
            if (!UnityEngine.Input.anyKeyDown)
                return;

            OnRestartPressed?.Invoke();
        }
    }
}
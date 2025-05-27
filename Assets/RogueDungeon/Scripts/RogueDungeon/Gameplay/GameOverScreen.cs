using UI;
using UnityEngine;
using Screen = UI.Screen;
using UI_Screen = UI.Screen;

namespace Gameplay
{
    public class GameOverScreen : UI_Screen
    {
        private Gameplay _gameplay;

        protected override DrawOrder DrawOrder => DrawOrder.GameOver;

        public void Construct(Gameplay gameplay) =>
            _gameplay = gameplay;

        private void Update()
        {
            if (!UnityEngine.Input.anyKeyDown)
                return;

            _gameplay.Restart();
            Destroy();
        }
    }
}
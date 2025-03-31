using RogueDungeon.UI;
using UnityEngine;
using Screen = RogueDungeon.UI.Screen;

namespace Gameplay
{
    public class GameOverScreen : Screen
    {
        private Gameplay _gameplay;

        protected override DrawOrder DrawOrder => DrawOrder.GameOver;

        public void Construct(Gameplay gameplay) =>
            _gameplay = gameplay;

        private void Update()
        {
            if (!Input.anyKeyDown)
                return;

            _gameplay.Restart();
            Destroy();
        }
    }
}
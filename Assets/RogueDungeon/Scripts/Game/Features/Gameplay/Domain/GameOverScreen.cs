namespace Game.Features.Gameplay.Domain
{
    public class GameOverScreen : Screen
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
        }
    }
}
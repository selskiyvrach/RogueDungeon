using Zenject;

namespace RogueDungeon.Game
{
    public class GameStateFactory : IGameStatesFactory
    {
        private readonly DiContainer _diContainer;

        public GameStateFactory(DiContainer diContainer) => 
            _diContainer = diContainer;

        public T Create<T>() where T : IGameState => 
            _diContainer.Instantiate<T>();
    }
}
using RogueDungeon.Services.FSM;

namespace RogueDungeon.Game
{
    public class GameStateChanger : IGameStateChanger
    {
        private readonly IGameStatesFactory _gameStateFactory;
        private IGameState _currentState;

        public GameStateChanger(IGameStatesFactory gameStateFactory) => 
            _gameStateFactory = gameStateFactory;

        public void EnterState<T>() where T : IGameState
        {
            (_currentState as IExitable)?.Exit();
            _currentState = _gameStateFactory.Create<T>();
            _currentState.Enter();
        }
    }
}
using Common.UI.LoadingScreen;
using Common.ZenjectUtils.ContextHandles;

namespace Common.Game
{
    public class GameStateChanger : IGameStateChanger
    {
        private readonly GameContextHandle _gameContextHandle;
        private readonly ILoadingScreen _loadingScreen;
        private GameState _currentState;

        public GameStateChanger(GameContextHandle gameContextHandle, ILoadingScreen loadingScreen)
        {
            _gameContextHandle = gameContextHandle;
            _loadingScreen = loadingScreen;
        }

        public async void EnterState<T>() where T : GameState
        {
            _loadingScreen.Show();
            _currentState = _gameContextHandle.Container.Instantiate<T>();
            await _currentState.Enter();
            _loadingScreen.Hide();
        }
    }
}
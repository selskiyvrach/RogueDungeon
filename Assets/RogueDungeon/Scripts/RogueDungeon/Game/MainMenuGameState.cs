using Common.FSM;
using Common.Game;
using Zenject;

namespace RogueDungeon.Game
{
    public class MainMenuGameState : IGameState, IExitable
    {
        private readonly IGameStateChanger _stateChanger;
        private readonly DiContainer _container;
        private readonly ISceneLoader _sceneLoader;
        private IMainMenuModel _mainMenuModel;

        public MainMenuGameState(IGameStateChanger stateChanger, DiContainer container, ISceneLoader sceneLoader)
        {
            _stateChanger = stateChanger;
            _container = container;
            _sceneLoader = sceneLoader;
        }

        public async void Enter()
        {
            await _sceneLoader.Load<MainMenuScene>();
            _mainMenuModel = _container.Resolve<MainMenuStateInstaller>().Resolve(); 
            _mainMenuModel.OnNewGame += StartNewGame;
            _mainMenuModel.OnQuit += Quit;
        }

        private void StartNewGame() => 
            _stateChanger.EnterState<GameplayGameState>();

        private void Quit() => 
            _stateChanger.EnterState<QuitGameState>();

        public void Exit() => 
            _mainMenuModel.Dispose();
    }
}
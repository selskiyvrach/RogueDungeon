using RogueDungeon.Services.FSM;
using RogueDungeon.UI.MainMenu;
using Zenject;

namespace RogueDungeon.Game
{
    public class MainMenuGameState : IGameState, IExitable
    {
        private readonly IGameStateChanger _stateChanger;
        private readonly IMainMenuModel _mainMenuModel;
        private readonly IMainMenuViewModel _viewModel;
        private readonly IFactory<IMainMenuViewModel, IMainMenuView> _viewFactory;
        private IMainMenuView _view;

        public MainMenuGameState(IGameStateChanger stateChanger, IMainMenuModel mainMenuModel, IMainMenuViewModel viewModel, IFactory<IMainMenuViewModel, IMainMenuView> viewFactory)
        {
            _stateChanger = stateChanger;
            _mainMenuModel = mainMenuModel;
            _viewModel = viewModel;
            _viewFactory = viewFactory;
        }

        public void Enter()
        {
            _view = _viewFactory.Create(_viewModel);
            _mainMenuModel.OnNewGame += StartNewGame;
            _mainMenuModel.OnQuit += Quit;
        }

        private void Quit() => 
            _stateChanger.EnterState<QuitGameState>();

        private void StartNewGame() => 
            _stateChanger.EnterState<GameplayGameState>();

        public void Exit()
        {
            _view.Dispose();
            _mainMenuModel.Dispose();
        }
    }
}
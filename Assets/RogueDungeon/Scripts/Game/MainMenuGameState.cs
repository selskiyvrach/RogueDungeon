namespace RogueDungeon.Game
{
    public class MainMenuGameState : IGameState
    {
        private readonly IGameStateChanger _stateChanger;
        private readonly IMainMenuModel _mainMenuModel;

        public void Enter()
        {
            _mainMenuModel.OnNewGame += StartNewGame;
            _mainMenuModel.OnQuit += Quit;
        }

        public MainMenuGameState(IGameStateChanger stateChanger, IMainMenuModel model)
        {
            _stateChanger = stateChanger;
            _mainMenuModel = model;
        }

        private void Quit() => 
            _stateChanger.EnterState<QuitGameState>();

        private void StartNewGame() => 
            _stateChanger.EnterState<GameplayGameState>();

        public void Exit() => 
            _mainMenuModel.Dispose();
    }
}
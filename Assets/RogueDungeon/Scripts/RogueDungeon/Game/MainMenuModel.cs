using Common.Commands;
using Common.Game;
using Common.Mvvm.Model;

namespace RogueDungeon.Game
{
    public class MainMenuModel : Model, IMainMenuModel
    {
        private readonly IGameStateChanger _gameStateChanger;

        public MainMenuModel(IGameStateChanger gameStateChanger) => 
            _gameStateChanger = gameStateChanger;

        public ICommand StartNewGameCommand() => 
            new ActionCommand(() => _gameStateChanger.EnterState<GameplayState>());

        public ICommand QuitCommand() => 
            new ActionCommand(() => _gameStateChanger.EnterState<QuitGameState>());
    }
}
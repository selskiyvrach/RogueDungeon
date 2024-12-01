using Common.Commands;
using Common.Mvvm.Model;

namespace RogueDungeon.Game
{
    public interface IMainMenuModel : IModel
    {
        ICommand StartNewGameCommand();
        ICommand QuitCommand();
    }
}
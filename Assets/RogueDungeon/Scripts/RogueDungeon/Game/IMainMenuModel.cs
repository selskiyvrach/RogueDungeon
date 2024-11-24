using System;
using System.Collections.Generic;
using Common.Commands;
using Common.Mvvm.Model;
using Common.UiCommons;

namespace RogueDungeon.Game
{
    public interface IMainMenuModel : IModel
    {
        event Action OnNewGame;
        event Action OnQuit;
        ICommand StartNewGameCommand();
        ICommand QuitCommand();
    }
}
using System;
using System.Collections.Generic;
using Common.Commands;
using Common.Mvvm.Model;
using Common.UiCommons;
using RogueDungeon.Localization;
using RogueDungeon.UI;

namespace RogueDungeon.Game
{
    public class MainMenuModel : Model, IMainMenuModel
    {
        public event Action OnNewGame;
        public event Action OnQuit;
        
        public ICommand StartNewGameCommand() => 
            new ActionCommand(() => OnNewGame.Invoke());

        public ICommand QuitCommand() => 
            new ActionCommand(() => OnQuit.Invoke());

        public override void Dispose()
        {
            base.Dispose();
            OnNewGame = null;
            OnQuit = null;
        }
    }
}
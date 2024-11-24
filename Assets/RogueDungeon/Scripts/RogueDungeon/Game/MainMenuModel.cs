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

        public IEnumerable<IMenuItem> MainItems { get; }

        public MainMenuModel() =>
            MainItems = new []
            {
                new MenuItemData(new ActionCommand(() => OnNewGame.Invoke()), Aliases.NEW_GAME),
                new MenuItemData(new ActionCommand(() => OnQuit.Invoke()), Aliases.QUIT_GAME),
            };

        public override void Dispose()
        {
            base.Dispose();
            OnNewGame = null;
            OnQuit = null;
        }
    }
}
using System;
using System.Collections.Generic;
using RogueDungeon.Services;
using RogueDungeon.Services.Commands;
using RogueDungeon.UI;
using RogueDungeon.UI.Common;

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
                new MenuItemData(new ActionCommand(OnNewGame.Invoke), Aliases.NEW_GAME),
                new MenuItemData(new ActionCommand(OnQuit.Invoke), Aliases.QUIT_GAME),
            };

        public override void Dispose()
        {
            base.Dispose();
            OnNewGame = null;
            OnQuit = null;
        }
    }
}
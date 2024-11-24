using System;
using System.Collections.Generic;
using RogueDungeon.UI;
using RogueDungeon.UI.Common;

namespace RogueDungeon.Game
{
    public interface IMainMenuModel : IModel
    {
        event Action OnNewGame;
        event Action OnQuit;
        IEnumerable<IMenuItem> MainItems { get; }
    }
}
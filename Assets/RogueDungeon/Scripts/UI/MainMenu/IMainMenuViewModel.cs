using RogueDungeon.Game;
using RogueDungeon.UI.Common;
using UniRx;

namespace RogueDungeon.UI.MainMenu
{
    public interface IMainMenuViewModel : IViewModel<IMainMenuModel>
    {
        IReadOnlyReactiveCollection<IMenuItem> MenuItems { get; }
    }
}
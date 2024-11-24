using Common.Mvvm.ViewModel;
using Common.UiCommons;
using RogueDungeon.Game;
using UniRx;

namespace RogueDungeon.UI.MainMenu
{
    public interface IMainMenuViewModel : IViewModel<IMainMenuModel>
    {
        IReadOnlyReactiveCollection<IMenuItem> MenuItems { get; }
    }
}
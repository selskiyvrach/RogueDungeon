using RogueDungeon.UI.Common;
using UniRx;

namespace RogueDungeon.UI.MainMenu
{
    public interface IMainMenuViewModel : IViewModel
    {
        IReadOnlyReactiveCollection<IMenuItem> MenuItems { get; }
    }
}
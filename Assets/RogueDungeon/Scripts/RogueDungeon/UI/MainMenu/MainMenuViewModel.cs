using Common.Mvvm.ViewModel;
using Common.UiCommons;
using RogueDungeon.Game;
using UniRx;

namespace RogueDungeon.UI.MainMenu
{
    public class MainMenuViewModel : ViewModel, IMainMenuViewModel
    {
        private readonly IMainMenuModel _mainMenuModel;
        public IReadOnlyReactiveCollection<IMenuItem> MenuItems { get; }

        public MainMenuViewModel(IMainMenuModel mainMenuModel)
        {
            _mainMenuModel = mainMenuModel;
            MenuItems = new ReactiveCollection<IMenuItem>(_mainMenuModel.MainItems);
        }

        public override void Dispose()
        {
            base.Dispose();
            _mainMenuModel?.Dispose();
        }
    }
}
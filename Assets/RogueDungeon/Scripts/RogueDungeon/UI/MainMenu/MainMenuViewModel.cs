using Common.Events;
using Common.Mvvm.ViewModel;
using Common.UiCommons;
using RogueDungeon.Game;
using RogueDungeon.Localization;
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
            MenuItems = new ReactiveCollection<IMenuItem>(new []
            {
                new MenuItemData(_mainMenuModel.StartNewGameCommand(), Aliases.NEW_GAME),
                new MenuItemData(_mainMenuModel.QuitCommand(), Aliases.QUIT_GAME)
            });
        }

        public override void Dispose()
        {
            base.Dispose();
            _mainMenuModel?.Dispose();
        }
    }
}
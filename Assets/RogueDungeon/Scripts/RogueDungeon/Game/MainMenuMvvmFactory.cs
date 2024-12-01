using Common.Mvvm.Zenject;
using RogueDungeon.UI.MainMenu;

namespace RogueDungeon.Game
{
    public class MainMenuMvvmFactory : 
        MvvmFactory<MainMenuModel, MainMenuViewModel, MainMenuView, IMainMenuModel, MainMenuMvvmFactory>
    {
    }
}
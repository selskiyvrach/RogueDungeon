using RogueDungeon.UI.LoadingScreen;
using RogueDungeon.UI.MainMenu;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Game
{
    [CreateAssetMenu(menuName = "Installers/MainMenuMvvmFactory", fileName = "MainMenuMvvmFactory", order = 0)]
    public class MainMenuMvvmFactory : MvvmFactory<MainMenuModel, MainMenuViewModel, MainMenuView, IMainMenuModel>
    {
        
    }
}
using Common.Mvvm.Zenject;
using Common.SceneManagement;
using RogueDungeon.UI.LoadingScreen;
using UnityEngine;

namespace RogueDungeon.Game
{
    [CreateAssetMenu(menuName = "Installers/LoadSceneMvvmFactory", fileName = "LoadSceneMvvmFactory", order = 0)]
    public class SceneLoadingProcessMvvmFactory : MvvmFactory<SceneLoadingModel, LoadingScreenViewModel, LoadingScreenView, ISceneLoadingModel>
    {
        
    }
}
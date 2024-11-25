using Common.Mvvm.Zenject;
using RogueDungeon.UI.LoadingScreen;
using UnityEngine;

namespace Common.SceneManagement
{
    [CreateAssetMenu(menuName = "Installers/LoadSceneMvvmFactory", fileName = "LoadSceneMvvmFactory", order = 0)]
    public class SceneLoadingProcessMvvmFactory : MvvmFactory<SceneLoadingModel, LoadingScreenViewModel, LoadingScreenView, ISceneLoadingModel>
    {
        
    }
}
using RogueDungeon.SceneManagement;
using UnityEngine;

namespace RogueDungeon.UI.LoadingScreen
{
    [CreateAssetMenu(menuName = "Installers/LoadSceneMvvmFactory", fileName = "LoadSceneMvvmFactory", order = 0)]
    public class SceneLoadingProcessMvvmFactory : MvvmFactory<SceneLoadingModel, SceneLoadingProcessViewModel, LoadingScreenView, ISceneLoadingModel>
    {
        
    }
}
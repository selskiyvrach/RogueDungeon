using Common.Mvvm.Zenject;
using RogueDungeon.UI.LoadingScreen;

namespace Common.SceneManagement
{
    public class SceneLoadingProcessMvvmFactory : 
        MvvmFactory<SceneLoadingModel, LoadingScreenViewModel, LoadingScreenView, ISceneLoadingModel, SceneLoadingProcessMvvmFactory>
    {
    }
}
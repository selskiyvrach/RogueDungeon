using System;
using RogueDungeon.UI.LoadingScreen;
using Zenject;

namespace RogueDungeon.SceneManagement
{
    public class SceneLoader : ISceneLoader
    {
        private readonly IFactory<ILoadingProcessViewModel, ILoadingScreenView> _viewFactory;
        private readonly IFactory<ILoadingModel, ILoadingProcessViewModel> _viewModelFactory;
        
        public SceneLoader(IFactory<ILoadingProcessViewModel, ILoadingScreenView> viewFactory, IFactory<ILoadingModel, ILoadingProcessViewModel> viewModelFactoryFactory)
        {
            _viewFactory = viewFactory;
            _viewModelFactory = viewModelFactoryFactory;
        }

        public void LoadScene<T>(Action callback = null) where T : Scene, new()
        {
            var model = new SceneLoadingModel();
            _viewFactory.Create(_viewModelFactory.Create(model));
            model.Load(new T().SceneName);  
        }
    }
}
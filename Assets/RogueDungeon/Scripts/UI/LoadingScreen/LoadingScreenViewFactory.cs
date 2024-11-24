using RogueDungeon.Game;
using UnityEngine;
using Zenject;

namespace RogueDungeon.UI.LoadingScreen
{
    public class LoadingScreenViewFactory : IFactory<ILoadingProcessViewModel, LoadingScreenView>
    {
        private readonly LoadingScreenView _prefab;
        private readonly IUiRootObject _rootObject;

        public LoadingScreenViewFactory(LoadingScreenView prefab, IUiRootObject rootObject)
        {
            _prefab = prefab;
            _rootObject = rootObject;
        }

        public LoadingScreenView Create(ILoadingProcessViewModel param)
        {
            var view = Object.Instantiate(_prefab, _rootObject.UiRootTransform);
            view.Initialize(param);
            return view;
        }
    }
}
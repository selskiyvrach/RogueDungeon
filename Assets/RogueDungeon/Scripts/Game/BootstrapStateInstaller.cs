using RogueDungeon.SceneManagement;
using RogueDungeon.UI.LoadingScreen;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Game
{
    [CreateAssetMenu(menuName = "Installers/BootstrapStateInstaller", fileName = "BootstrapStateInstaller", order = 0)]
    public class BootstrapStateInstaller : ScriptableObjectInstaller<BootstrapStateInstaller>
    {
        [SerializeField] private GameRootObject _gameRootGameObject;
        [SerializeField] private LoadingScreenView _loadingScreenView;

        public override void InstallBindings()
        {
            var gameRoot = Instantiate(_gameRootGameObject);
            DontDestroyOnLoad(gameRoot);
            Container.Bind<LoadingScreenView>().FromInstance(_loadingScreenView).AsSingle();
            Container.BindInterfacesTo<GameRootObject>().FromInstance(gameRoot).AsSingle();
            Container.Bind<IFactory<ILoadingScreenViewModel, ILoadingScreenView>>().To<LoadingScreenViewFactory>().AsSingle();
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
        }
    }
}
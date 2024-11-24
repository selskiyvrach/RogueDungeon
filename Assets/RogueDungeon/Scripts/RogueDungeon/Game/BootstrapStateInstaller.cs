using Common.SceneManagement;
using Common.UnityUtils;
using RogueDungeon.Camera;
using RogueDungeon.UI.LoadingScreen;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace RogueDungeon.Game
{
    [CreateAssetMenu(menuName = "Installers/BootstrapStateInstaller", fileName = "BootstrapStateInstaller", order = 0)]
    public class BootstrapStateInstaller : ScriptableObjectInstaller<BootstrapStateInstaller>
    {
        [SerializeField] private GameRootObject _rootGameObject;
        [SerializeField] private GameCamera _gameCamera;
        [SerializeField] private EventSystem _eventSystem;
        [SerializeField] private SceneLoadingProcessMvvmFactory _loadingSceneMvvmFactory;

        public override void InstallBindings()
        {
            var gameRoot = Instantiate(_rootGameObject);
            DontDestroyOnLoad(gameRoot);
            Container.BindInterfacesTo<GameRootObject>().FromInstance(gameRoot).AsSingle();

            var coroutineRunner = new GameObject("CoroutineRunner").AddComponent<CoroutineRunner>();
            coroutineRunner.transform.SetParent(gameRoot.CommonRootTransform);
            Container.Bind<ICoroutineRunner>().To<CoroutineRunner>().FromInstance(coroutineRunner).AsSingle();
            
            Instantiate(_eventSystem, gameRoot.CommonRootTransform);

            Container.Bind<IGameCamera>().To<GameCamera>().FromInstance(Instantiate(_gameCamera, gameRoot.CommonRootTransform));

            Container.Bind<IFactory<ISceneLoadingModel>>().To<SceneLoadingProcessMvvmFactory>().FromNewScriptableObject(_loadingSceneMvvmFactory).AsSingle();
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
        }
    }
}
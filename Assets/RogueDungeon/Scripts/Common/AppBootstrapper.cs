using Common.Camera;
using Common.SceneManagement;
using Common.UI.LoadingScreen;
using Common.UtilsUnity;
using Common.UtilsZenject;
using Common.UtilsZenject.ContextHandles;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Common
{
    public class AppBootstrapper : MonoInstaller
    {
        [SerializeField] private LoadingScreen _loadingScreenPrefab;
        [SerializeField] private GameCamera _gameCameraPrefab;
        [SerializeField] private EventSystem _eventSystemPrefab;

        private GameContextHandle _gameContext;
        private GameObject _gameRootObject;
        
        public override void InstallBindings()
        {
            ResolveGameRootContext();
            CreateRootObject();
            CreateCameraAndLoadingScreen();
            CreateCoroutineRunner();
            CreateSceneLoader();
            CreateEventSystem();
            _gameContext.Container.Resolve<Game.Game>().Start();
        }

        private void CreateEventSystem() => 
            _gameContext.Container.Bind<EventSystem>().FromInstance(Instantiate(_eventSystemPrefab, _gameRootObject.transform));

        private void CreateCameraAndLoadingScreen()
        {
            _gameContext.Container.Bind<ILoadingScreen>().FromInstance(Instantiate(_loadingScreenPrefab, _gameRootObject.transform));
            _gameContext.Container.Bind<GameCamera>().FromInstance(Instantiate(_gameCameraPrefab, _gameRootObject.transform));
        }

        private void CreateSceneLoader() => 
            _gameContext.Container.NewSingle<ISceneLoader, SceneLoader>();

        private void ResolveGameRootContext() => 
            _gameContext = Container.Resolve<GameContextHandle>();

        private void CreateCoroutineRunner()
        {
            var coroutineRunner = new GameObject("CoroutineRunner", typeof(CoroutineRunner)).GetComponent<CoroutineRunner>();
            coroutineRunner.transform.SetParent(_gameRootObject.transform);
            _gameContext.Container.Bind<ICoroutineRunner>().FromInstance(coroutineRunner);
        }

        private void CreateRootObject()
        {
            _gameRootObject = new GameObject("GameRoot");
            _gameRootObject.gameObject.isStatic = true;
            DontDestroyOnLoad(_gameRootObject);
        }
    }
}
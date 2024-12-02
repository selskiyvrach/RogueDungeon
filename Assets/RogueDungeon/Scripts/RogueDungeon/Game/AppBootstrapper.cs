using Common.GameObjectMarkers;
using Common.SceneManagement;
using Common.UnityUtils;
using Common.ZenjectUtils;
using Common.ZenjectUtils.ContextHandles;
using RogueDungeon.Camera;
using RogueDungeon.UI.LoadingScreen;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace RogueDungeon.Game
{
    public class AppBootstrapper : MonoInstaller
    {
        [SerializeField] private LoadingScreen _loadingScreenPrefab;
        [SerializeField] private GameCamera _gameCameraPrefab;
        [SerializeField] private EventSystem _eventSystemPrefab;

        private GameContextHandle _gameContext;
        private GameRootObject _gameRootObject;
        
        public override void InstallBindings()
        {
            ResolveGameRootContext();
            CreateRootObject();
            CreateCameraAndLoadingScreen();
            CreateCoroutineRunner();
            CreateSceneLoader();
            CreateEventSystem();
            _gameContext.Container.Resolve<Common.Game.Game>().Start();
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
            _gameRootObject = new GameObject("GameRoot", typeof(GameRootObject)).GetComponent<GameRootObject>();
            _gameRootObject.gameObject.isStatic = true;
            DontDestroyOnLoad(_gameRootObject);
            _gameContext.Container.Bind<GameRootObject>().FromInstance(_gameRootObject);
        }
    }
}
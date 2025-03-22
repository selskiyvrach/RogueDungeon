using Common.SceneManagement;
using Common.UI;
using Common.UI.Bars;
using Common.UI.LoadingScreen;
using Common.Unity;
using Common.UtilsDotNet;
using Common.UtilsZenject;
using RogueDungeon.Camera;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using Zenject;

namespace RogueDungeon.Game
{
    public class ProjectContextInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private LoadingScreen _loadingScreenPrefab;
        [SerializeField] private GameCamera _gameCameraPrefab;
        [SerializeField] private EventSystem _eventSystemPrefab;

        private GameObject _gameRootObject;

        public override void InstallBindings()
        {
            CreateRootObject();
            CreateCameraAndLoadingScreen();
            CreateCoroutineRunner();
            CreateSceneLoader();
            CreateEventSystem();
        }

        private void CreateEventSystem() => 
            Container.Bind<EventSystem>().FromInstance(Instantiate(_eventSystemPrefab, _gameRootObject.transform));

        private void CreateCameraAndLoadingScreen()
        {
            Container.Bind<ILoadingScreen>().FromInstance(Instantiate(_loadingScreenPrefab, _gameRootObject.transform));
            Container.Bind<IGameCamera>().FromInstance(Instantiate(_gameCameraPrefab, _gameRootObject.transform));
        }

        private void CreateSceneLoader() => 
            Container.NewSingle<ISceneLoader, SceneLoader>();

        private void CreateCoroutineRunner()
        {
            var coroutineRunner = new GameObject("CoroutineRunner", typeof(CoroutineRunner)).GetComponent<CoroutineRunner>();
            coroutineRunner.transform.SetParent(_gameRootObject.transform);
            Container.Bind<ICoroutineRunner>().FromInstance(coroutineRunner);
        }

        private void CreateRootObject()
        {
            _gameRootObject = new GameObject("GameRoot");
            _gameRootObject.gameObject.isStatic = true;
            DontDestroyOnLoad(_gameRootObject);
        }
    }
}
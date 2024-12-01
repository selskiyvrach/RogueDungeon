using Common.Events;
using Common.GameObjectMarkers;
using Common.InstallerGenerator;
using Common.SceneManagement;
using Common.UnityUtils;
using RogueDungeon.Camera;
using RogueDungeon.UI.LoadingScreen;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Game
{
    public class BootstrapperInstaller : MonoInstaller
    {
        [SerializeField] private GameRootObject _gameRootObject;
        [SerializeField] private GameCamera _gameCamera;
        [SerializeField] private LoadingScreen loadingScreen;

        public override void InstallBindings() => 
            Container.Resolve<BootstrapGameState>().BoostrapInstallerIsReady(this);

        public void Bootstrap(DiContainer gameContext)
        {
            DontDestroyOnLoad(_gameRootObject);
            gameContext.Bind<GameRootObject>().FromInstance(_gameRootObject).AsSingle();
            
            var coroutineRunner = new GameObject("CoroutineRunner").AddComponent<CoroutineRunner>();
            coroutineRunner.transform.SetParent(_gameRootObject.transform);
            gameContext.InstanceSingle<ICoroutineRunner, CoroutineRunner>(coroutineRunner);
            
            gameContext.NewSingle<IEventBus, EventBus>();
            gameContext.InstanceSingle<IGameCamera, GameCamera>(_gameCamera);
            gameContext.NewSingle<ISceneLoader, SceneLoader>();
        }
    }
}
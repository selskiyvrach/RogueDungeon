using Common.Events;
using Common.InstallerGenerator;
using Common.SceneManagement;
using Common.UnityUtils;
using RogueDungeon.Camera;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace RogueDungeon.Game
{
    [CreateAssetMenu(menuName = "Installers/BootstrapStateInstaller", fileName = "BootstrapStateInstaller", order = 0)]
    public class BootstrapStateInstaller : ScriptableInstaller
    {
        [SerializeField] private GameRootObject _rootGameObject;
        [SerializeField] private GameCamera _gameCamera;
        [SerializeField] private EventSystem _eventSystem;

        public override void Install(DiContainer container)
        {
            var gameRoot = Instantiate(_rootGameObject);
            DontDestroyOnLoad(gameRoot);
            container.BindInterfacesTo<GameRootObject>().FromInstance(gameRoot).AsSingle();
            
            var coroutineRunner = new GameObject("CoroutineRunner").AddComponent<CoroutineRunner>();
            coroutineRunner.transform.SetParent(gameRoot.CommonRootTransform);
            container.InstanceSingle<ICoroutineRunner, CoroutineRunner>(coroutineRunner);
            
            container.NewSingle<IEventBus, EventBus>();
            Instantiate(_eventSystem, gameRoot.CommonRootTransform);
            
            // base needs some stuff
            base.Install(container);
            
            container.InstanceSingle<IGameCamera, GameCamera>(Instantiate(_gameCamera, gameRoot.CommonRootTransform));
            container.NewSingle<ISceneLoader, SceneLoader>();
        }
    }
}
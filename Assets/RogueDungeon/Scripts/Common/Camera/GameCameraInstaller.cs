using UnityEngine;
using Zenject;

namespace Common.Camera
{
    public class GameCameraInstaller : ScriptableObjectInstaller<GameCameraInstaller>
    {
        [SerializeField] private GameCamera _camera;
        
        public override void InstallBindings() => 
            Container.Bind<IGameCamera>().To<GameCamera>().FromComponentInNewPrefab(_camera).AsSingle();
    }
}
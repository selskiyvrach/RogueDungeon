using UnityEngine;
using Zenject;

namespace RogueDungeon.Camera
{
    [CreateAssetMenu(menuName = "Installers/GameCameraInstaller", fileName = "GameCameraInstaller", order = 0)]
    public class GameCameraInstaller : ScriptableObjectInstaller<GameCameraInstaller>
    {
        [SerializeField] private GameCamera _camera;
        
        public override void InstallBindings() => 
            Container.Bind<IGameCamera>().To<GameCamera>().FromComponentInNewPrefab(_camera).AsSingle();
    }
}
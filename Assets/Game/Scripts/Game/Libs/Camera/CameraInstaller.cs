using Libs.Utils.DotNet;
using UnityEngine;
using Zenject;

namespace Game.Libs.Camera
{
    public class CameraInstaller : MonoInstaller
    {
        [SerializeField] private UnityEngine.Camera _camera;

        public override void InstallBindings() => 
            Container.Bind<UnityEngine.Camera>().FromInstance(_camera.ThrowIfNull()).AsSingle();
    }
}
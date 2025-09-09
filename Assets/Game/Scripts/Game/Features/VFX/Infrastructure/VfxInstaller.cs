using Game.Features.VFX.App;
using UnityEngine;
using Zenject;

namespace Game.Features.VFX.Infrastructure
{
    public class VfxInstaller : MonoInstaller
    {
        [SerializeField] private CameraShakerConfig _config;
        [SerializeField] private BloodScreen _bloodScreen;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<CameraShaker>().AsSingle().WithArguments(_config);
            Container.BindInterfacesTo<PlayVfxOnCombatEventsUseCase>().AsSingle().NonLazy();
            Container.BindInterfacesTo<BloodScreen>().FromInstance(_bloodScreen).AsSingle();
        }
    }
}
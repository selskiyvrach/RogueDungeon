using Game.Features.VFX.App;
using UnityEngine;
using Zenject;

namespace Game.Features.VFX.Infrastructure
{
    public class VfxInstaller : MonoInstaller
    {
        [SerializeField] private CameraShakerConfig _config;
        [SerializeField] private BloodScreen _bloodScreen;
        [SerializeField] private HitFlasher _hitFlasher;
        [SerializeField] private HitFlasherConfig _hitFlasherConfig;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<CameraShaker>().AsSingle().WithArguments(_config);
            Container.BindInterfacesTo<PlayVfxOnCombatEventsUseCase>().AsSingle().NonLazy();
            Container.BindInterfacesTo<BloodScreen>().FromInstance(_bloodScreen).AsSingle();
            Container.BindInterfacesTo<HitFlasherConfig>().FromInstance(_hitFlasherConfig).AsSingle();
            Container.BindInterfacesTo<HitFlasher>().FromInstance(_hitFlasher).AsSingle();
        }
    }
}
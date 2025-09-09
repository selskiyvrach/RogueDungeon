using Game.Features.Combat.Domain.Enemies;
using Game.Features.Combat.Domain.Enemies.HiveMind;
using Libs.Utils.Zenject;
using UnityEngine;
using Zenject;

namespace Game.Features.Combat.Infrastructure
{
    public class HiveMindInstaller : MonoInstaller
    {
        [SerializeField] private HiveMindConfig _config;

        public override void InstallBindings()
        {
            var container = Container.CreateSubContainer();
            container.InstanceSingle(_config);
            container.NewSingle<HiveMind>();
            Container.Bind<HiveMind>().FromSubContainerResolve().ByInstance(container).AsSingle();
        }
    }
}
using Game.Features.Enemies.Domain.HiveMind;
using UnityEngine;
using Zenject;

namespace Game.Features.Enemies.Installers
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
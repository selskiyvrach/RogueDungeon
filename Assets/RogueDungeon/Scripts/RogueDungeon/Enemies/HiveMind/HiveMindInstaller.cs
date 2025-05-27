using Common.UtilsZenject;
using UnityEngine;
using Zenject;

namespace Enemies.HiveMind
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
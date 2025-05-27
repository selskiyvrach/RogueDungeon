using Common.UtilsZenject;
using UnityEngine;
using Zenject;

namespace Input
{
    public class PlayerInputInstaller : MonoInstaller
    {
        [SerializeField] private InputMapConfig _config;
        
        public override void InstallBindings()
        {
            var container = Container.CreateSubContainer();
            container.InstanceSingle(_config);
            container.NewSingleInterfacesAndSelf<InputMap>();
            container.NewSingleInterfacesAndSelf<PlayerInput>();
            
            Container.Bind<IPlayerInput>().FromSubContainerResolve().ByInstance(container).AsSingle();
        }
    }
}
using Game.Libs.Time;
using Libs.Utils.Zenject;
using UnityEngine;
using Zenject;

namespace Game.Libs.Input
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
            
            Container.Bind<IPlayerInput>().FromMethod(() =>
            {
                var input = container.Resolve<IPlayerInput>();
                container.Resolve<IGameTime>().StartTicking(input, TickOrder.Input);
                return input;
            }).AsSingle();
        }
    }
}
using Common.MoveSets;
using Common.UtilsZenject;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Player.Behaviours.Movement
{
    public class PlayerMovementBehaviourInstaller : MonoInstaller
    {
        [SerializeField] private MoveSetConfig _moveSetConfig;

        public override void InstallBindings()
        {
            var container = Container.CreateSubContainer();
            container.InstanceSingle(_moveSetConfig);
            container.InstanceSingle(new MoveSetFactory(container).Create(_moveSetConfig));
            container.NewSingleInterfacesAndSelf<PlayerMovementBehaviour>();
            Container.Bind<PlayerMovementBehaviour>().FromSubContainerResolve().ByInstance(container).AsSingle();
        }
    }
}
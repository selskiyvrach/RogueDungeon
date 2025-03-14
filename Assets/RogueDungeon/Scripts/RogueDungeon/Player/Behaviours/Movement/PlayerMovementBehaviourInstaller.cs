using Common.Fsm;
using Common.UtilsZenject;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Player.Behaviours.Movement
{
    public class PlayerMovementBehaviourInstaller : MonoInstaller
    {
        [SerializeField] private LevelTraverserConfig _levelTraverserConfig;

        public override void InstallBindings()
        {
            var levelTraverserContainer = Container.CreateSubContainer();
            levelTraverserContainer.InstanceSingle(_levelTraverserConfig);
            levelTraverserContainer.NewSingle<ITypeBasedStatesProvider, TypeBasedStatesProviderWithCache>();
            levelTraverserContainer.NewSingleInterfacesAndSelf<TypeBasedTransitionStrategy>();
            levelTraverserContainer.Resolve<TypeBasedTransitionStrategy>().SetStartState<TraversalIdleState>();
            levelTraverserContainer.NewSingle<StateMachine>();
            levelTraverserContainer.NewSingleInterfacesAndSelf<PlayerMovement>();
            Container.Bind<IPlayerMovementBehaviour>().FromSubContainerResolve().ByInstance(levelTraverserContainer).AsSingle();
        }
    }
}
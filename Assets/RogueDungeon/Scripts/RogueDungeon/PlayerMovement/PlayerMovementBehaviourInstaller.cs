using Common.Fsm;
using Common.UtilsZenject;
using RogueDungeon.Player;
using UnityEngine;
using Zenject;

namespace PlayerMovement
{
    public class PlayerMovementBehaviourInstaller : MonoInstaller
    {
        [SerializeField] private LevelTraverserConfig _levelTraverserConfig;

        public override void InstallBindings()
        {
            var levelTraverserContainer = Container.CreateSubContainer();
            levelTraverserContainer.InstanceSingle(_levelTraverserConfig);
            levelTraverserContainer.NewSingle<ITypeBasedStatesProvider, TypeBasedStatesProviderWithCache>();
            levelTraverserContainer.NewSingle<IStateTransitionStrategy, TypeBasedTransitionStrategy>();
            levelTraverserContainer.Resolve<TypeBasedTransitionStrategy>().SetStartState<TraversalIdleState>();
            levelTraverserContainer.NewSingle<StateMachine>();
            levelTraverserContainer.NewSingleInterfacesAndSelf<PlayerMovement>();
            Container.Bind<IPlayerMovementBehaviour>().FromSubContainerResolve().ByInstance(levelTraverserContainer).AsSingle();
        }
    }
}
using Common.Animations;
using Common.Fsm;
using Common.UtilsZenject;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Enemies
{
    public class EnemyLifeCycleBehaviourInstaller : MonoInstaller
    {
        [SerializeField] private AnimationPlayer _animationPlayer;
        [SerializeField] private EnemyLifeCycleConfig _config;

        public override void InstallBindings()
        {
            var container = Container.CreateSubContainer();
            
            container.InstanceSingle<IAnimator>(_animationPlayer);
            container.InstanceSingle(_config);
            container.NewSingle<EnemylifeCycleMoveSetContext>();
            container.NewSingle<ITypeBasedStatesProvider, StatesProviderWithCache>();
            var transitionStrategy = container.Instantiate<TypeBasedTransitionStrategy>().SetStartState<EnemyBirthState>();
            container.InstanceSingle<IStateTransitionStrategy>(transitionStrategy);
            container.NewSingle<StateMachine>();
            container.NewSingle<EnemyLifeCycleMoveSetBehaviour>();
            Container.Bind<EnemyLifeCycleMoveSetBehaviour>().FromSubContainerResolve().ByInstance(container).AsSingle();
        }
    }
}
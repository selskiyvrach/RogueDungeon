using Common.Fsm;
using Common.UtilsZenject;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Enemies.HiveMind
{
    public class HiveMindInstaller : MonoInstaller
    {
        [SerializeField] private HiveMindConfig _config;

        public override void InstallBindings()
        {
            var container = Container.CreateSubContainer();
            container.InstanceSingle(_config);
            container.NewSingle<HiveMindContext>();
            container.NewSingle<ITypeBasedStatesProvider, TypeBasedStatesProviderWithCache>();
            var transitions = container.Instantiate<TypeBasedTransitionStrategy>();
            transitions.SetStartState<HiveMindIdleState>();
            container.NewSingle<StateMachine>();
            container.InstanceSingle<IStateTransitionStrategy>(transitions);
            container.NewSingle<HiveMindBehaviour>();
            Container.Bind<HiveMindBehaviour>().FromSubContainerResolve().ByInstance(container).AsSingle();
        }
    }
}
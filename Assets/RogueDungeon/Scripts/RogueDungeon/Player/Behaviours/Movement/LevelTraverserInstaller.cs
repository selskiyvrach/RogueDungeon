using Common.Fsm;
using Common.UtilsZenject;
using UnityEngine;
using Zenject;

namespace RogueDungeon.Player.Behaviours.Movement
{
    public class LevelTraverserInstaller : MonoInstaller
    {
        [SerializeField] private LevelTraverserConfig _levelTraverserConfig;

        public override void InstallBindings()
        {
            var levelTraverserContainer = Container.CreateSubContainer();
            levelTraverserContainer.InstanceSingle(_levelTraverserConfig);
            levelTraverserContainer.NewSingle<ITypeBasedStatesProvider, StatesProviderWithCache>();
            levelTraverserContainer.NewSingle<IStateTransitionStrategy, TypeBasedTransitionStrategy>();
            levelTraverserContainer.NewSingle<StateMachine>();
            levelTraverserContainer.NewSingleInterfacesAndSelf<LevelTraverserBehaviour>();
            Container.Bind<LevelTraverserBehaviour>().FromSubContainerResolve().ByInstance(levelTraverserContainer).AsSingle();
        }
    }
}
using Common.Fsm;
using Common.UtilsZenject;
using Zenject;

namespace Common.Behaviours
{
    public static class ZenjectExtensions
    {
        public static DiContainer BehaviourSubcontainer<TBehaviour>(this DiContainer container, bool autoRunBehaviour = false) where TBehaviour : IBehaviour => 
            container.BehaviourSubcontainer<TBehaviour, NullInternalFacade, NullExternalFacade>(autoRunBehaviour);
        
        public static DiContainer BehaviourSubcontainer<TBehaviour, TInternalFacade>(this DiContainer container, bool autoRunBehaviour = false) where TBehaviour : IBehaviour 
            where TInternalFacade : IBehaviourInternalFacade =>
            container.BehaviourSubcontainer<TBehaviour, TInternalFacade, NullExternalFacade>(autoRunBehaviour);
        
        public static DiContainer BehaviourSubcontainer<TBehaviour, TInternalFacade, TExternalFacade>(
            this DiContainer container, bool autoRunBehaviour = false) 
            where TBehaviour : IBehaviour 
            where TExternalFacade : IBehaviourExternalFacade 
            where TInternalFacade : IBehaviourInternalFacade
        {
            var behaviourContainer = container.CreateSubContainer();
            
            if(typeof(TInternalFacade) != typeof(NullInternalFacade))
                behaviourContainer.NewSingleInterfaces<TInternalFacade>();

            if(typeof(TExternalFacade) != typeof(NullExternalFacade))
                container.BindInterfacesTo<TExternalFacade>().FromMethod(_ => behaviourContainer.Instantiate<TExternalFacade>()).AsSingle();
            
            behaviourContainer.NewSingle<ITypeBasedStatesProvider, StatesProviderWithCache>();
            behaviourContainer.NewSingleInterfacesAndSelf<TBehaviour>();

            if (autoRunBehaviour) 
                behaviourContainer.NewSingleAutoResolve<BehaviourInitializer<TBehaviour>>();
            
            return behaviourContainer;
        }
        
        public static void AutoInitBehaviour<TBehaviour>(this DiContainer container) where TBehaviour : IBehaviour => 
            container.NewSingle<BehaviourInitializer<TBehaviour>>();
    }
}
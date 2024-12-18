using Zenject;

namespace Common.UtilsZenject
{
    public static class ZenjectExtensions
    {
        public static ConcreteIdArgConditionCopyNonLazyBinder InstanceSingle<TAs, TTo>(this DiContainer container, TTo instance) where TTo : TAs => 
            container.Bind<TAs>().To<TTo>().FromInstance(instance).AsSingle();

        public static ConcreteIdArgConditionCopyNonLazyBinder InstanceSingleInterfacesAndSelf<TTo>(this DiContainer container, TTo instance) => 
            container.BindInterfacesAndSelfTo<TTo>().FromInstance(instance).AsSingle();

        public static ConcreteIdArgConditionCopyNonLazyBinder InstanceSingleInterfaces<TTo>(this DiContainer container, TTo instance) => 
            container.BindInterfacesTo<TTo>().FromInstance(instance).AsSingle();

        public static ConcreteIdArgConditionCopyNonLazyBinder InstanceSingle<T>(this DiContainer container, T instance) => 
            container.Bind<T>().FromInstance(instance).AsSingle();

        public static ConcreteIdArgConditionCopyNonLazyBinder NewSingle<TAs, TTo>(this DiContainer container) where TTo : TAs => 
            container.Bind<TAs>().To<TTo>().FromNew().AsSingle();
        
        public static IfNotBoundBinder NewSingleNonLazy<TAs, TTo>(this DiContainer container) where TTo : TAs => 
            container.NewSingle<TAs, TTo>().NonLazy();
        
        public static ConcreteIdArgConditionCopyNonLazyBinder NewSingleInterfaces<TTo>(this DiContainer container) => 
            container.BindInterfacesTo<TTo>().FromNew().AsSingle();
        
        public static ConcreteIdArgConditionCopyNonLazyBinder NewSingleInterfacesAndSelf<TTo>(this DiContainer container) => 
            container.BindInterfacesAndSelfTo<TTo>().FromNew().AsSingle();
        
        public static ConcreteIdArgConditionCopyNonLazyBinder NewSingle<T>(this DiContainer container) =>
            container.NewSingle<T, T>();

        public static IfNotBoundBinder NewSingleNonLazy<T>(this DiContainer container) =>
            container.NewSingleNonLazy<T, T>();
        
        public static T NewSingleResolve<T>(this DiContainer container)
        {
            container.NewSingle<T>();
            return container.Resolve<T>();
        }

        public static void NewSingeDecorator<T, TDecorator>(this DiContainer container) where TDecorator : T
        {
            var decorated = container.Resolve<T>();
            container.NewSingle<T, TDecorator>().WithArguments(decorated);
        }
    }
}
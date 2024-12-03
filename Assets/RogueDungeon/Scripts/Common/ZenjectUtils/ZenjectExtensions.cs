using Zenject;

namespace Common.ZenjectUtils
{
    public static class ZenjectExtensions
    {
        public static TTo InstanceSingle<TAs, TTo>(this DiContainer container, TTo instance) where TTo : TAs
        {
            container.Bind<TAs>().To<TTo>().FromInstance(instance);
            return instance;
        }
        
        public static TTo InstanceSingleInterfacesAndSelf<TTo>(this DiContainer container, TTo instance)
        {
            container.BindInterfacesTo<TTo>().FromInstance(instance);
            return instance;
        }

        public static TTo InstanceSingle<TAs1, TAs2, TTo>(this DiContainer container, TTo instance) where TTo : TAs1, TAs2
        {
            container.Bind<TAs1>().To<TTo>().FromInstance(instance);
            container.Bind<TAs2>().To<TTo>().FromInstance(instance);
            return instance;
        }

        public static T InstanceSingle<T>(this DiContainer container, T instance)
        {
            container.Bind<T>().FromInstance(instance);
            return instance;
        }

        public static void NewSingle<TAs, TTo>(this DiContainer container) where TTo : TAs => 
            container.Bind<TAs>().To<TTo>().FromNew().AsSingle();
        
        public static void NewSingleInterfaces<TTo>(this DiContainer container) => 
            container.BindInterfacesTo<TTo>().FromNew().AsSingle();
        
        public static void NewSingleInterfacesAndSelf<TTo>(this DiContainer container) => 
            container.BindInterfacesAndSelfTo<TTo>().FromNew().AsSingle();
        
        public static void NewSingle<T>(this DiContainer container) =>
            container.NewSingle<T, T>();

        public static T NewSingleResolve<T>(this DiContainer container)
        {
            container.NewSingle<T>();
            return container.Resolve<T>();
        }
    }
}
using Zenject;

namespace Common.ZenjectUtils
{
    public static class ZenjectExtensions
    {
        public static void InstanceSingle<TAs, TTo>(this DiContainer container, TTo instance) where TTo : TAs => 
            container.Bind<TAs>().To<TTo>().FromInstance(instance);
        
        public static void InstanceSingle<T>(this DiContainer container, T instance) => 
            container.Bind<T>().FromInstance(instance);

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
using System;
using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Libs.Utils.Zenject
{
    public static class ZenjectExtensions
    {
        /// <summary>
        /// Handles interfaces and base classes unlike zenject bindInterfacesAndSelf since the latter does not call GetType on the received object, using the type it has been passed as
        /// </summary>
        public static void BindAllInterfacesAndBaseClasses<T>(this DiContainer container, T instance)
        {
            var actualType = instance.GetType();
            var interfaces = actualType.GetInterfaces();
            var baseTypes = GetBaseTypes(actualType);

            foreach (var type in interfaces.Concat(baseTypes)) 
                container.Bind(type).FromInstance(instance).AsCached();

            container.Bind(actualType).FromInstance(instance).AsCached();
            return;

            IEnumerable<Type> GetBaseTypes(Type type)
            {
                type = type.BaseType;
                while (type != null && type != typeof(object))
                {
                    yield return type;
                    type = type.BaseType;
                }
            }
        }
        
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

        public static InstantiateCallbackConditionCopyNonLazyBinder NewSingeDecorator<T, TDecorator>(this DiContainer container, params object[] arguments) where TDecorator : T
        {
            var decorated = container.Resolve<T>();
            return container.NewSingle<T, TDecorator>().WithArguments(decorated);
        }
    }
}
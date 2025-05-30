using System;
using Zenject;

namespace Libs.Parameters
{
    public static class ZenjectExtensions
    {
        public static void NewSingleParameter<T>(this DiContainer container, Func<float> baseValueGetter) where T : IParameterDefinition =>
            container.Bind<IParameter<T>>().FromMethod(
                () => container.TryResolve<IParameterModifier<T>>() is { } modifier
                    ? new ModifiedParameter<T>(new Parameter<T>(baseValueGetter), modifier)
                    : new Parameter<T>(baseValueGetter)).AsSingle();
    }
}
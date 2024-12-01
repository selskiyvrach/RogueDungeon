using System;

namespace Common.InstallerGenerator
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class CreateFactoryInstallerAttribute : Attribute
    {
        public Type BindAs { get; }
        public Type[] SerializedParams { get; }
        public Type[] FactoryParams { get; }

        public CreateFactoryInstallerAttribute(Type bindAs = null, Type[] serializedParams = null, Type[] factoryParams = null)
        {
            BindAs = bindAs;
            SerializedParams = serializedParams;
            FactoryParams = factoryParams;
        }
    }
}
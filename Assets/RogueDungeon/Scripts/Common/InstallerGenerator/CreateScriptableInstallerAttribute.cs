using System;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class CreateScriptableInstallerAttribute : Attribute
{
    public string Name { get; }
    public Type BindAs { get; }
    public Type[] SerializedFields { get; }

    public CreateScriptableInstallerAttribute(string name = null, Type bindAs = null, Type[] serializedFields = null)
    {
        Name = name;
        BindAs = bindAs;
        SerializedFields = serializedFields;
    }
}
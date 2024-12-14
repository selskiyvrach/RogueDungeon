using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Common.Animations;

public static class AnimatorParameterUtility
{
    public static List<string> GetParametersByAttribute<T>(Type type) where T : Attribute
    {
        if (!type.IsClass || !type.IsAbstract || !type.IsSealed)
            throw new ArgumentException("Type must be a static class.", nameof(type));

        return type.GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(f => f.IsLiteral && !f.IsInitOnly && f.FieldType == typeof(string) && f.IsDefined(typeof(T), false))
            .Select(f => (string)f.GetValue(null))
            .ToList();
    }

    public static List<Type> GetMarkedClasses()
    {
        return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.IsDefined(typeof(AnimatorParametersAttribute), false))
            .ToList();
    }
}
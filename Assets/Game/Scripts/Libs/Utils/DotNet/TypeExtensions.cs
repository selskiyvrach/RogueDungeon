using System;
using System.Collections.Generic;

namespace Libs.Utils.DotNet
{
    public static class TypeExtensions
    {
        public static IEnumerable<Type> GetAllBaseTypesAndInterfaces(this Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            var allTypes = new HashSet<Type>();

            var current = type;
            while (current != null && current != typeof(object))
            {
                allTypes.Add(current);
                current = current.BaseType;
            }

            foreach (var interfaceType in type.GetInterfaces()) 
                allTypes.Add(interfaceType);

            return allTypes;
        }
    }
}
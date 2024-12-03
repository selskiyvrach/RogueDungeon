using System.Collections.Generic;

namespace Common.Registries
{
    public class Registry : Registry<object>, IRegistry
    {
    }

    public class Registry<TConstraint> : List<TConstraint>, IRegistry<TConstraint>
    {
    }
}
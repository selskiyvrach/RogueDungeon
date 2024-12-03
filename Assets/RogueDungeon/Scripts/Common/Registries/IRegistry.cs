using System.Collections.Generic;

namespace Common.Registries
{
    public interface IRegistry : IRegistry<object>
    {
    }

    public interface IRegistry<TConstraint> : IList<TConstraint>
    {
    }
}
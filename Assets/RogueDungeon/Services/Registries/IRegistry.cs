using System;
using System.Collections.Generic;

namespace RogueDungeon.Services.Registries
{
    public interface IRegistry<TConstraint> : IReadonlyRegistry<TConstraint>, IDisposable
    {
        void Add(TConstraint entity);
        void Remove(TConstraint entity);
    }

    public interface IReadonlyRegistry<TConstraint> : IEnumerable<TConstraint>
    {
        
    }
}
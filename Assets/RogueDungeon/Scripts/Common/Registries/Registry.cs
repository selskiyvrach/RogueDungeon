using System.Collections.Generic;

namespace Common.Registries
{
    public class Registry<TConstraint> : List<TConstraint>, IRegistry<TConstraint>, IRegistryNode<TConstraint>
    {
        private readonly List<IRegistry<TConstraint>> _children = new();
        
        public new void Add(TConstraint entity) => 
            base.Add(entity);

        public new void Remove(TConstraint entity) => 
            base.Remove(entity);

        public void AddChildren(params IRegistry<TConstraint>[] children)
        {
            foreach (var registry in children)
            {
                _children.Add(registry);
            }
        }

        public void RemoveChildren(params IRegistry<TConstraint>[] children)
        {
            foreach (var registry in children)
            {
                _children.Remove(registry);
            }
        }

        public void Dispose() => 
            Clear();
    }
}
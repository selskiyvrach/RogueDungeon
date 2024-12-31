using System;

namespace Common.Transactions
{
    public abstract class ResourceBank<TResource, TNumericValue> : IResourceBank<TResource> 
        where TResource : IResourceDefinition, TNumericValue
        where TNumericValue : INumericValue
    {
        public Type ResourceDefinition => typeof(TResource);
        
        public bool HasEnough(IResourceCost cost) => 
            HasEnough((TResource)cost);

        public void Spend(IResourceCost cost) => 
            Spend((TResource)cost);

        public void Replenish(IResourceCost amount) => 
            Replenish((TResource)amount);

        public abstract bool HasEnough(TResource cost);
        public abstract void Spend(TResource cost);
        public abstract void Replenish(TResource amount);
    }
}
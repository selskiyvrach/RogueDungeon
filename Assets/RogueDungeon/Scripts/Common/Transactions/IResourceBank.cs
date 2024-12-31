using System;

namespace Common.Transactions
{
    public interface IResourceBank<T> : IResourceBank where T : IResourceDefinition
    {
    }

    public interface IResourceBank
    {
        Type ResourceDefinition { get; }
        bool HasEnough(IResourceCost cost);
        void Spend(IResourceCost cost);
        void Replenish(IResourceCost amount);

        virtual bool TrySpend(IResourceCost cost)
        {
            if (HasEnough(cost))
                Spend(cost);
            return HasEnough(cost);
        }
    }
}
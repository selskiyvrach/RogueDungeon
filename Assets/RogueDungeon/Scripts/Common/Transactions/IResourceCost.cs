using System;

namespace Common.Transactions
{
    public interface IResourceCost<T> : IResourceCost where T : IResourceDefinition
    {
        Type IResourceCost.ResourceDefinition => typeof(T);
    }

    public interface IResourceCost
    {
        Type ResourceDefinition { get; }
        IResourceCost Combine(IResourceCost other);
    }
}
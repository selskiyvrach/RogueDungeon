using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Common.Transactions
{
    public readonly struct CostsAggregator : IEnumerable<IResourceCost>
    {
        private readonly IEnumerable<IResourceCost> _costs;

        public CostsAggregator(IEnumerable<IResourceCost> costs) => 
            _costs = costs;

        public IEnumerator<IResourceCost> GetEnumerator()
        {
            foreach (var group in _costs.GroupBy(cost => cost.ResourceDefinition))
            {
                yield return group.Aggregate((a, b) => a.Combine(b));
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            GetEnumerator();
    }
}
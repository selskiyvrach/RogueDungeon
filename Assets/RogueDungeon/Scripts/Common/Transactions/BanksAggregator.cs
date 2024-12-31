using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Common.Transactions
{
    public readonly struct BanksAggregator : IEnumerable<IResourceBank>
    {
        private readonly IEnumerable<IResourceBank> _banks;

        public BanksAggregator(IEnumerable<IResourceBank> banks) => 
            _banks = banks;

        public IEnumerator<IResourceBank> GetEnumerator() => 
            _banks.ToDictionary(n => n.ResourceDefinition, n => n).Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => 
            GetEnumerator();
    }
}
using System.Collections.Generic;
using System.Linq;

namespace Common.Transactions
{
    public readonly struct TransactionFacilitator
    {
        private readonly IEnumerable<IResourceBank> _banks;
        private readonly IEnumerable<IResourceCost> _costs;

        public TransactionFacilitator(IEnumerable<IResourceBank> banks, IEnumerable<IResourceCost> costs)
        {
            _banks = banks;
            _costs = costs;
        }

        public bool TryPerformTransaction()
        {
            var banks = new BanksAggregator(_banks);
            var costs = new CostsAggregator(_costs);
            var costBankPairs = costs.Select(n => (n, banks.First(b => b.ResourceDefinition == n.ResourceDefinition))).ToArray();
            foreach (var (cost, bank) in costBankPairs)
                if (!bank.HasEnough(cost))
                    return false;
            foreach (var (cost, bank) in costBankPairs)
                bank.Spend(cost);
            return true;
        }
    }
}
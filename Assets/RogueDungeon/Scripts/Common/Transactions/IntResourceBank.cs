namespace Common.Transactions
{
    public class IntResourceBank<TResource> : ResourceBank<TResource, IIntValue> where TResource : IResourceDefinition, IIntValue
    {
        private int _storedAmount;

        public override bool HasEnough(TResource cost) => 
            cost.Value <= _storedAmount;

        public override void Spend(TResource cost) => 
            _storedAmount -= cost.Value;

        public override void Replenish(TResource amount) => 
            _storedAmount += amount.Value;
    }
}
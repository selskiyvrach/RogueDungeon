namespace Common.Transactions
{
    public class FloatResourceBank<TResource> : ResourceBank<TResource, IFloatValue> where TResource : IResourceDefinition, IFloatValue
    {
        private float _storedAmount;

        public override bool HasEnough(TResource cost) => 
            cost.Value <= _storedAmount;

        public override void Spend(TResource cost) => 
            _storedAmount -= cost.Value;

        public override void Replenish(TResource amount) => 
            _storedAmount += amount.Value;
    }
}
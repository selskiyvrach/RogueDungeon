using Common.Lifecycle;

namespace RogueDungeon.Characters
{
    public class RechargeableResource : Resource, ITickable
    {
        private readonly RechargeableResourceConfig _config;
        private float _timeSinceSpent;

        public RechargeableResource(RechargeableResourceConfig config) : base(config) => 
            _config = config;


        public override void AddDelta(float value)
        {
            base.AddDelta(value);
            if(_config.RechargeRate > 0 != value > 0)
                _timeSinceSpent = 0;
        }

        public void Tick(float deltaTime)
        {
            if (_timeSinceSpent < _config.RechargeDelay)
            {
                _timeSinceSpent += deltaTime;
                return;
            }

            if(_config.RechargeRate == 0)
                return;
            if(_config.RechargeRate < 0 && Current > 0)
                AddDelta(_config.RechargeRate * deltaTime);
            else if(_config.RechargeRate > 0 && Current < Max)
                AddDelta(_config.RechargeRate * deltaTime);
        }
    }
}
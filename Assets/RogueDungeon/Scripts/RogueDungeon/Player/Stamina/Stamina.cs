using System;
using Characters;

namespace RogueDungeon.Player.Stamina
{
    public class Stamina : IResource
    {
        private readonly StaminaConfig _config;
        private float _timeSinceSpent;
        
        public float Current { get; private set; }
        public float Max => _config.Max;
        public event Action OnChanged;

        public Stamina(StaminaConfig config) => 
            _config = config;

        public void Tick(float deltaTime)
        {
            _timeSinceSpent += deltaTime;
            if(_timeSinceSpent <= _config.RechargeDelay || Current >= Max)
                return;
            Current += _config.RechargeRate * deltaTime;
            if(Current > Max)
                Current = Max;
            OnChanged?.Invoke();
        }

        public void Spend(float amount)
        {
            if(amount <= 0)
                return;
            
            Current -= amount;
            if(Current < 0)
                Current = 0;
            
            _timeSinceSpent = 0;
            OnChanged?.Invoke();
        }

        public void Refill()
        {
            Current = Max;
            OnChanged?.Invoke();
        }
    }

}
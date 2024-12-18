using System;

namespace RogueDungeon.Behaviours.MovementBehaviour
{
    public interface IDodgeTimingsProvider
    {
        float GetDuration();
    }
    
    public class DummyDodgeTimingsProvider : IDodgeTimingsProvider
    {
        private readonly Func<float> _value;

        public DummyDodgeTimingsProvider(Func<float> value) => 
            _value = value;

        public float GetDuration() => 
            _value.Invoke();
    }
}
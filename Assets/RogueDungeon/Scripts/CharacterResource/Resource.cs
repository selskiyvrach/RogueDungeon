using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace RogueDungeon.CharacterResource
{
    public class Resource
    {
        public float Current { get; private set; }

        public float Max { get; private set; }
        public bool IsDepleted => Current <= 0;

        public event System.Action<ResourceChangeReason> OnChanged;

        public bool IsFull => Math.Abs(Current - Max) < .001;

        public void Spend(float amount)
        {
            Assert.IsTrue(amount >= 0);
            Set(Max, Current - amount, ResourceChangeReason.Spent);
        }

        public void Set(float max, float current, ResourceChangeReason reason)
        {
            Max = max;
            Current = Mathf.Clamp(current, 0, Max);
            OnChanged?.Invoke(reason);
        }
        
        public void Set(float maxAndCurrent, ResourceChangeReason reason) => 
            Set(maxAndCurrent, maxAndCurrent, reason);

        public void Restore(float amount)
        {
            Assert.IsTrue(amount >= 0);
            Set(Max, Current + amount, ResourceChangeReason.Restored);
        }
    }
}
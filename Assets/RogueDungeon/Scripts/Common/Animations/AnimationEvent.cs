using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Common.Animations
{
    [Serializable]
    public struct AnimationEvent
    {
        [field: SerializeField] public float Time { get; private set; }
        [field: SerializeField] public string Name { get; private set; }

        public AnimationEvent(float time, string name)
        {
            Assert.IsTrue(time is >= 0 and <= 1);
            Time = time;
            Name = name;
        }
    }
}
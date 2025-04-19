using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions;

namespace Common.Animations
{
    [Serializable]
    public struct AnimationEvent
    {
        [field:HorizontalGroup, SerializeField] public string Name { get; private set; }
        [field:HorizontalGroup, Range(0, 1), SerializeField] public float Time { get; private set; }

        public AnimationEvent(float time, string name)
        {
            Assert.IsTrue(time is >= 0 and <= 1);
            Time = time;
            Name = name;
        }
    }
}
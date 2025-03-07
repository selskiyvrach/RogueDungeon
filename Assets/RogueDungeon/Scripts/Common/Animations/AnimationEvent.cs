using System;
using UnityEngine;

namespace Common.Animations
{
    [Serializable]
    public struct AnimationEvent
    {
        [field: SerializeField] public float Time { get; private set; }
        [field: SerializeField] public string Name { get; private set; }

        public AnimationEvent(float time, string name)
        {
            Time = time;
            Name = name;
        }
    }
}
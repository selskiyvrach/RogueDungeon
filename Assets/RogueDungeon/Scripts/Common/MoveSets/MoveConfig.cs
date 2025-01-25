using System;
using UnityEngine;

namespace Common.MoveSets
{
    [Serializable]
    public class MoveConfig
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public AnimationClip Animation { get; private set; }
        [field: SerializeField] public float Duration { get; private set; } = .5f;
        [field: SerializeField] public string[] Transitions { get; private set; }
    }
}
using System;
using UnityEngine;

namespace RogueDungeon.MoveSets
{
    public class MoveConfig : ScriptableObject
    {
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] public AnimationClip AnimationClip { get; private set; }
        [field: SerializeField] public float Duration { get; private set; }
        [field: SerializeField] public string[] Transitions { get; private set; }

        public virtual Type MoveType { get; } = typeof(Move);
    }
}
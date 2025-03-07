using System;
using System.Collections.Generic;
using System.Linq;
using Common.Animations;
using Sirenix.OdinInspector;
using UnityEngine;
using AnimationEvent = Common.Animations.AnimationEvent;

namespace Common.MoveSets
{
    public class MoveConfig : ScriptableObject
    {
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] public AnimationConfigPicker AnimationConfigPicker { get; private set; }
        [field: SerializeField] public float Duration { get; private set; }
        [field: SerializeField] public string[] Transitions { get; private set; }

        public virtual Type MoveType { get; } = typeof(Move);
    }
}
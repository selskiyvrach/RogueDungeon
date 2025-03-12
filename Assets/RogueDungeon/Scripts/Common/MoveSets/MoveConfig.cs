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
        [field: SerializeField] public bool IsLooping { get; private set; }
        [field: SerializeField, HideLabel] public AnimationConfigPicker AnimationConfigPicker { get; private set; }
        [field: SerializeField] public TransitionPicker[] Transitions { get; private set; }

        public virtual Type MoveType { get; } = typeof(Move);
    }
}
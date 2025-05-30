using System;
using Libs.Animations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Libs.Movesets
{
    public abstract class MoveConfig : ScriptableObject
    {
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] public bool IsLooping { get; private set; }
        [field: SerializeField, HideLabel] public AnimationConfigPicker AnimationConfigPicker { get; private set; }
        [field: SerializeField] public TransitionPicker[] Transitions { get; private set; }

        public virtual Type MoveType { get; } = typeof(Move);

        public MoveCreationArgs ToCreationArgs() => 
            new(Id, MoveType, AnimationConfigPicker.Config, Transitions);
    }
}
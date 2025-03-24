using System;
using Common.Animations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RogueDungeon.Enemies.States
{
    public abstract class EnemyStateConfig : ScriptableObject
    {
        [field: SerializeField] public Priority Priority { get; private set; }
        [field: SerializeField, HideLabel] public AnimationConfigPicker AnimationConfigPicker { get; private set; }
        [field: SerializeField, HideLabel] public AnimationConfigPicker[] AdditionalAnimations { get; private set; }
        public virtual Type StateType => typeof(EnemyState);
    }
}
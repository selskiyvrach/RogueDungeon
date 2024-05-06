using System;
using JetBrains.Annotations;
using RogueDungeon.Characters;
using RogueDungeon.Data;
using RogueDungeon.Data.Stats;
using RogueDungeon.Items;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RogueDungeon.Actions
{
    public interface IActionConfig
    {
        string AnimationName { get; }
        int Frames { get; }
        bool Cycle { get; }
        string GetKeyframe(int frame);
    }

    [Serializable]
    public class ActionConfig : IActionConfig
    {
        [SerializeField] private string _animationName;
        [SerializeField] private StatConfig _frames;
        [SerializeField] private bool _cycle;
        [SerializeField] private Keyframe[] _keyframes;

        public string AnimationName => _animationName;
        public int Frames => _frames.GetIntValue();
        public bool Cycle => _cycle;

        [CanBeNull]
        public string GetKeyframe(int frame)
        {
            if (!(frame > 0 && frame <= _frames.GetIntValue())) 
                Debug.Log("Keyframe is out of range");

            foreach (var keyframe in _keyframes)
            {
                if (keyframe.Frame.GetIntValue() == frame)
                    return keyframe.Name;
            }

            return null;
        }
    }
        
    [Serializable]
    public class HandyActionConfig : IAttackConfig, IActionConfig
    {
        public enum damageType
        {
            SlashDamage = 1,
            PierceDamage = 2,
            BluntDamage = 3,
        }

        public enum HitType
        {
            Left,
            Right,
            Middle,
        }

        [SerializeField] private string _id;
        [HorizontalGroup]
        [SerializeField] private RelativeValue _damage = RelativeValue.Medium;
        [HorizontalGroup, HideLabel]
        [SerializeField] private damageType _damageType = damageType.PierceDamage;

        [SerializeField] private HitType _hitType = HitType.Left;
        [SerializeField] private RelativeValue _duration = RelativeValue.Medium;

        public string Id => _id;
        public string DamageType => _damageType.ToString();
        public float Damage => StandardValues.GetValue("AttackDamage", _damage);
        public int Frames => Mathf.RoundToInt(StandardValues.GetValue("AttackActionDuration", _duration));
        public bool Cycle => false;

        public DodgeState DodgeableBy => _hitType switch
        {
            HitType.Left => DodgeState.DodgingRight,
            HitType.Right => DodgeState.DodgingLeft,
            HitType.Middle => DodgeState.NotDodging,
            _ => throw new ArgumentOutOfRangeException(),
        };

        public IActionConfig AttackActionConfig => this;

        public string AnimationName => _hitType switch
        {
            HitType.Left => "AttackLeft",
            HitType.Right => "AttackRight",
            HitType.Middle => "AttackCenter",
            _ => throw new ArgumentOutOfRangeException(),
        };

        public string GetKeyframe(int frame) =>
            frame == Mathf.RoundToInt(StandardValues.GetValue("AttackActionHitKeyframe", _duration)) 
                ? "Hit" 
                : null;

        public AttackAction CreateAction() => 
            new(this);
    }
}
using System;
using RogueDungeon.Actions;
using RogueDungeon.Characters;
using RogueDungeon.Data.Stats;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RogueDungeon.Items
{
    [Serializable]
    public class AttackConfig : IAttackConfig
    {
        [SerializeField] private StatConfig _damage;
        [SerializeField] private StatConfig _balanceDamage;
        [field: SerializeField] public string DamageType { get; private set; }
        [field: SerializeField] public DodgeState DodgeableBy { get; private set; } = DodgeState.NotDodging;
        [SerializeField] private ActionConfig _attackActionConfig;
        
        public IActionConfig AttackActionConfig => _attackActionConfig;
        public float BalanceDamage => _balanceDamage.GetValue();
        public float Damage => _damage.GetValue();
        public AttackAction CreateAction() => 
            new(this);
    }

    [Serializable]
    public class AttackComboConfig
    {
        [field: SerializeField] public AttackConfig BaseAttack { get; private set; }
        [field: SerializeField] public string[] Animations { get; private set; }
        [field: SerializeField] public bool UseCustomFinalAttack { get; private set; }
        [field: ShowIf("_useCustomFinalAttack"), SerializeField] public AttackConfig FinalAttack { get; private set; }
    }
    
    public class Combo : IAttackConfig, IActionConfig
    {
        private readonly AttackComboConfig _config;
        private int _currentIndex;
        private AttackConfig _currentAttack;

        public int CurrentIndex
        {
            get => _currentIndex;
            set
            {
                _currentIndex = value;
                _currentAttack = _currentIndex == _config.Animations.Length - 1 && _config.UseCustomFinalAttack
                        ? _config.FinalAttack
                        : _config.BaseAttack;
            }
        }

        public float Damage => _currentAttack.Damage;
        public string DamageType => _currentAttack.DamageType;
        public float BalanceDamage => _currentAttack.BalanceDamage;
        public DodgeState DodgeableBy => _currentAttack.DodgeableBy;
        public IActionConfig AttackActionConfig => this;

        public string AnimationName => _config.Animations[_currentIndex] is null or ""
            ? _currentAttack.AttackActionConfig.AnimationName
            : _config.Animations[_currentIndex];
        
        public int Frames => _currentAttack.AttackActionConfig.Frames;
        public bool Cycle => false;

        public int Length => _config.Animations.Length;

        public Combo(AttackComboConfig config)
        {
            _config = config;
            CurrentIndex = 0;
        }

        public string GetKeyframe(int frame) => 
            _currentAttack.AttackActionConfig.GetKeyframe(frame);
    }
}
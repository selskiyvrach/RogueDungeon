using RogueDungeon.Animations;
using RogueDungeon.Configs;
using UnityEngine;

namespace RogueDungeon.Weapons
{
    [CreateAssetMenu(menuName = "Configs/Weapons/WeaponConfig", fileName = "WeaponConfig", order = 0)]
    public class WeaponConfig : Config
    {
        [Header("Stats")] 
        [SerializeField] private float _damage;
        
        [Header("Attacks")]
        [SerializeField] private AnimationConfig _idleToAttackAnimation;
        [SerializeField] private AnimationConfig[] _attackAnimations;
        [SerializeField] private AnimationConfig _toIdleAnimation;

        [Header("Block")]
        [SerializeField] private AnimationConfig _idleToBlockAnimation;
        [SerializeField] private AnimationConfig _blockToIdleAnimation;
        [SerializeField] private AnimationConfig _holdBlockAnimation;

        public float Damage => _damage;
        public AnimationConfig IdleToAttackAnimation => _idleToAttackAnimation;
        public AnimationConfig[] AttackAnimations => _attackAnimations;
        public AnimationConfig ToIdleAnimation => _toIdleAnimation;
        public AnimationConfig IdleToBlockAnimation => _idleToBlockAnimation;
        public AnimationConfig BlockToIdleAnimation => _blockToIdleAnimation;
        public AnimationConfig HoldBlockAnimation => _holdBlockAnimation;
    }
}
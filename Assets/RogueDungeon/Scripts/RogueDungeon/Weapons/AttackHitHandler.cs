using System.Linq;
using Common.DotNetUtils;
using RogueDungeon.Characters;
using RogueDungeon.Collisions;
using UniRx;

namespace RogueDungeon.Weapons
{
    public class AttackHitHandler
    {
        private readonly IAttackMediator _attackMediator;
        private readonly IWeaponParametersConfig _config;
        private readonly IAttackDamageModifier _attackDamageModifier;
        private readonly ICollisionsDetector _collisionsDetector;

        public AttackHitHandler(IAttackMediator attackMediator, IWeaponParametersConfig config, IAttackDamageModifier attackDamageModifier, ICollisionsDetector collisionsDetector)
        {
            _attackMediator = attackMediator;
            _config = config;
            _attackDamageModifier = attackDamageModifier;
            _collisionsDetector = collisionsDetector;
            _attackMediator.OnHitKeyframe.Subscribe(_ => HandleHit());
        }

        private void HandleHit() =>
            _collisionsDetector.GetCollisions(_config.GetPositionsHitMask(_attackMediator.AttackIndex))
                .Select(n => n.Collidable).GetAll<IDamageable>().Foreach(DealDamage);

        private void DealDamage(IDamageable damageable)
        {
            var damage = _config.GetDamage(_attackMediator.AttackIndex);
            _attackDamageModifier.ApplyModifiers(ref damage, damageable);
            damageable.TakeDamage(damage);
        }
    }
}
using System.Linq;
using Common.DotNetUtils;
using RogueDungeon.Characters;
using RogueDungeon.Collisions;
using Zenject;

namespace RogueDungeon.Weapons
{
    public class AttackHitHandler //: IInitializable
    {
        // private readonly IAttackBehaviour _attackBehaviour;
        // private readonly IAttackDamageModifier _attackDamageModifier;
        // private readonly ICollisionsDetector _collisionsDetector;
        //
        // public AttackHitHandler(IAttackDamageModifier attackDamageModifier, ICollisionsDetector collisionsDetector, IAttackBehaviour attackBehaviour)
        // {
        //     _attackDamageModifier = attackDamageModifier;
        //     _collisionsDetector = collisionsDetector;
        //     _attackBehaviour = attackBehaviour;
        // }
        //
        // public void Initialize() => 
        //     _attackBehaviour.OnHitKeyframe += HandleHit;
        //
        // private void HandleHit() =>
        //     _collisionsDetector.GetCollisions(_config.GetPositionsHitMask())
        //         .Select(n => n.Collidable).GetAll<IDamageable>().Foreach(DealDamage);
        //
        // private void DealDamage(IDamageable damageable)
        // {
        //     var damage = _config.GetDamage(_attackMediator.AttackIndex);
        //     _attackDamageModifier.ApplyModifiers(ref damage, damageable);
        //     damageable.TakeDamage(damage);
        // }
    }
}
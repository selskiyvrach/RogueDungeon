using System;
using Game.Features.Combat.Domain;
using Game.Features.Combat.Domain.Enemies;
using Game.Libs.Combat;
using Zenject;

namespace Game.Features.VFX.App
{
    public class PlayVfxOnCombatEventsUseCase : IInitializable, IDisposable
    {
        private readonly IHitFlasher _hitFlasher;
        private readonly IBloodScreen _bloodScreen;
        private readonly ICameraShaker _shaker;
        private readonly ICombatEvents _combatEvents;

        public PlayVfxOnCombatEventsUseCase(ICombatEvents combatEvents, ICameraShaker shaker, IBloodScreen bloodScreen, IHitFlasher hitFlasher)
        {
            _combatEvents = combatEvents;
            _shaker = shaker;
            _bloodScreen = bloodScreen;
            _hitFlasher = hitFlasher;
        }

        public void Initialize()
        {
            _combatEvents.OnPlayerAttackResult += HandlePlayerHit;
            _combatEvents.OnEnemyAttackResult += HandeEnemyHit;
        }

        public void Dispose()
        {
            _combatEvents.OnPlayerAttackResult -= HandlePlayerHit;
            _combatEvents.OnEnemyAttackResult -= HandeEnemyHit;
        }

        private void HandeEnemyHit(Enemy arg1, EnemyAttackResult arg2)
        {
            _shaker.DoShake(!arg2.IsHit ? ShakeIntensity.Mild :
                arg2.FinalDamage > 0 ? ShakeIntensity.Extreme : ShakeIntensity.Strong);
            if(arg2.FinalDamage > 0)
                _bloodScreen.Play();
            _hitFlasher.Play(arg2.AttackInfo.Position switch {
                EnemyPosition.Middle when arg2.AttackInfo.Direction is AttackDirection.Left => HitFlashPosition.FrontLeft,
                EnemyPosition.Middle when arg2.AttackInfo.Direction is AttackDirection.Right => HitFlashPosition.FrontRight,
                EnemyPosition.Left => HitFlashPosition.BackLeft,
                EnemyPosition.Right => HitFlashPosition.BackRight,
                _ => throw new ArgumentOutOfRangeException()
            });
        }

        private void HandlePlayerHit(Enemy arg1, PlayerAttackResult arg2) => 
            _shaker.DoShake(arg2.IsHit ? ShakeIntensity.Strong : ShakeIntensity.Mild);
    }
}
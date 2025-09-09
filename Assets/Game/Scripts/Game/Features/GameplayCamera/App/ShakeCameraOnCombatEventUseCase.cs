using System;
using Game.Features.Combat.Domain;
using Game.Features.Combat.Domain.Enemies;
using Game.Features.GameplayCamera.Domain;
using Game.Libs.Combat;
using Zenject;

namespace Game.Features.GameplayCamera.App
{
    public class ShakeCameraOnCombatEventUseCase : IInitializable, IDisposable
    {
        private readonly ICameraShaker _shaker;
        private readonly ICombatEvents _combatEvents;

        public ShakeCameraOnCombatEventUseCase(ICameraShaker shaker, ICombatEvents combatEvents)
        {
            _shaker = shaker;
            _combatEvents = combatEvents;
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

        private void HandeEnemyHit(Enemy arg1, EnemyAttackResult arg2) => 
            _shaker.DoShake(!arg2.IsHit ? ShakeIntensity.Mild : arg2.FinalDamage > 0 ? ShakeIntensity.Extreme : ShakeIntensity.Strong);

        private void HandlePlayerHit(Enemy arg1, PlayerAttackResult arg2) => 
            _shaker.DoShake(arg2.IsHit ? ShakeIntensity.Strong : ShakeIntensity.Mild);
    }
}
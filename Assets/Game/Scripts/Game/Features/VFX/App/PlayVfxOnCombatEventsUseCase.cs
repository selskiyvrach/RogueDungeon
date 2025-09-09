using System;
using Game.Features.Combat.Domain;
using Game.Features.Combat.Domain.Enemies;
using Game.Libs.Combat;
using Zenject;

namespace Game.Features.VFX.App
{
    public class PlayVfxOnCombatEventsUseCase : IInitializable, IDisposable
    {
        private readonly IBloodScreen _bloodScreen;
        private readonly ICameraShaker _shaker;
        private readonly ICombatEvents _combatEvents;

        public PlayVfxOnCombatEventsUseCase(ICombatEvents combatEvents, ICameraShaker shaker, IBloodScreen bloodScreen)
        {
            _combatEvents = combatEvents;
            _shaker = shaker;
            _bloodScreen = bloodScreen;
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
        }

        private void HandlePlayerHit(Enemy arg1, PlayerAttackResult arg2) => 
            _shaker.DoShake(arg2.IsHit ? ShakeIntensity.Strong : ShakeIntensity.Mild);
    }
}
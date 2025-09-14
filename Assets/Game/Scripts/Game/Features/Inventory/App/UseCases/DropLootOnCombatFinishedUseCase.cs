using System;
using Game.Features.Combat.Domain;
using Game.Features.Inventory.Domain;
using Game.Features.Player.Domain.Movesets.Movement;
using Libs.Utils.DotNet;
using Zenject;

namespace Game.Features.Inventory.App.UseCases
{
    public class DropLootOnCombatFinishedUseCase : IInitializable, IDisposable
    {
        private readonly ICombat _combat;
        private readonly ILootDropper _lootDropper;
        private readonly ICombatConfigsRepository _combatConfigsRepository;

        public DropLootOnCombatFinishedUseCase(ICombat combat, ILootDropper lootDropper, ICombatConfigsRepository combatConfigsRepository)
        {
            _combat = combat;
            _lootDropper = lootDropper;
            _combatConfigsRepository = combatConfigsRepository;
        }

        public void Initialize() => 
            _combat.OnFinished += DropLoot;

        public void Dispose() => 
            _combat.OnFinished -= DropLoot;

        private void DropLoot()
        {
            var lootId = _combatConfigsRepository.Get(_combat.Id).LootId;
            if(!lootId.IsNullOrEmpty())
                _lootDropper.DropLoot(lootId, _combat.Coordinates);
        }
    }
}
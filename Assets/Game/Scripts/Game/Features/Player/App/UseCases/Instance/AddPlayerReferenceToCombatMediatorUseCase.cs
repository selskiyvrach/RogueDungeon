using Game.Features.Combat.Domain;
using Game.Features.Player.Domain;
using Zenject;

namespace Game.Features.Player.App.UseCases.Instance
{
    public class AddPlayerReferenceToCombatMediatorUseCase
    {
        public AddPlayerReferenceToCombatMediatorUseCase(DiContainer container, PlayerSpawner playerSpawner, AttacksMediator attacksMediator)
        {
            // playerSpawner.OnPlayerSpawned += player => attacksMediator.PlayerDefenderInfoProvider = container.Instantiate<PlayerToDefenderInfoAdapter>(new object[]{player});
            // playerSpawner.OnPlayerDespawned += _ => attacksMediator.PlayerDefenderInfoProvider = null;
        }
    }
}
using Game.Features.Combat.Domain;

namespace Game.Features.Player.App.UseCases.Instance
{
    public class SetPlayerReferenceToAttacksMediatorUseCase
    {
        public SetPlayerReferenceToAttacksMediatorUseCase(Domain.Player player, AttacksMediator attacksMediator) => 
            attacksMediator.PlayerDefenderInfoProvider = player;
    }
}
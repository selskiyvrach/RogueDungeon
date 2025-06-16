using Game.Features.Combat.Domain;

namespace Game.Features.Player.App.UseCases.Instance
{
    public class MediateAttacksUseCase
    {
        public MediateAttacksUseCase(Domain.Player player, AttacksMediator attacksMediator)
        {
            player.OnAttackMediationRequested += attacksMediator.MediatePlayerAttack;
            attacksMediator.OnPlayerAttackResult += player.SetPlayerAttackResult;
            attacksMediator.OnEnemyAttackResult += player.SetEnemyAttackResult;
        }
    }
}
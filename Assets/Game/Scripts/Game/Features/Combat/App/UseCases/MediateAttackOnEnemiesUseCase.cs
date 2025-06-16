using Game.Features.Combat.Domain;

namespace Game.Features.Combat.App
{
    public class MediateAttacksOnEnemiesUseCase
    {
        public MediateAttacksOnEnemiesUseCase(AttacksMediator attacksMediator)
        {
            attacksMediator.OnPlayerAttackResult += (enemy, result) =>
            {
                enemy.SetPlayerAttackResult(result);
            };
            attacksMediator.OnEnemyAttackResult += (enemy, result) =>
            {
                enemy.SetOwnAttackResult(result);
            };
        }
    }
}
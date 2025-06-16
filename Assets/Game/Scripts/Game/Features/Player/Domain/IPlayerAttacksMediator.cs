using Game.Libs.Combat;

namespace Game.Features.Player.Domain
{
    public interface IPlayerAttacksMediator
    {
        void MediatePlayerAttack(PlayerAttackInfo attackInfo);
    }
}
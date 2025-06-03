using Game.Features.Player.Domain.Behaviours.Hands;

namespace Game.Features.Player.Domain
{
    public interface IPlayerAttacksMediator
    {
        void MediatePlayerAttack(IItem weapon);
    }
}
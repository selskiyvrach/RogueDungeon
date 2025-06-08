using Game.Libs.Items;

namespace Game.Features.Player.Domain
{
    public interface IPlayerAttacksMediator
    {
        void MediatePlayerAttack(IItem weapon);
    }
}
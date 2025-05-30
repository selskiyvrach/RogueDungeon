using Game.Features.Items.Domain;

namespace Game.Features.Player.Domain
{
    public interface IPlayerAttacksMediator
    {
        void MediatePlayerAttack(IWeapon weapon);
    }
}
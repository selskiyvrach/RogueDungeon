using RogueDungeon.Items;

namespace RogueDungeon.Player.Model
{
    public interface IPlayerAttacksMediator
    {
        void MediatePlayerAttack(IWeapon weapon);
    }
}
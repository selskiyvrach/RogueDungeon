using RogueDungeon.Items;

namespace RogueDungeon.Player
{
    public interface IPlayerAttacksMediator
    {
        void MediatePlayerAttack(IWeapon weapon);
    }
}
using RogueDungeon.Items.Model;

namespace Player.Model
{
    public interface IPlayerAttacksMediator
    {
        void MediatePlayerAttack(IWeapon weapon);
    }
}
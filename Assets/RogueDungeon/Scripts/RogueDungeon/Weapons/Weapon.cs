namespace RogueDungeon.Weapons
{
    public interface IWeapon
    {
    }

    public class Weapon : IWeapon
    {
        private readonly IAttackBehaviour _attackBehaviour;

        public Weapon(IAttackBehaviour attackBehaviour)
        {
            _attackBehaviour = attackBehaviour;
            _attackBehaviour.Enable();
        }
    }
}
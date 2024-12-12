namespace RogueDungeon.Weapons
{
    public interface IWeapon
    {
    }

    public class Weapon : IWeapon
    {
        private readonly IAttackBehaviour _attackBehaviour;
        private readonly AttackAnimatorController _animatorController;

        public Weapon(IAttackBehaviour attackBehaviour, AttackAnimatorController animatorController)
        {
            _attackBehaviour = attackBehaviour;
            _animatorController = animatorController;
            _attackBehaviour.Enable();
            _animatorController.Initialize();
        }
    }
}
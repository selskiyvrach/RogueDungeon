namespace RogueDungeon.Behaviours.WeaponBehaviour
{
    public abstract class AttackStateChangedHandler
    {
        private readonly IAttackMediator _attackMediator;

        protected AttackStateChangedHandler(IAttackMediator attackMediator)
        {
            _attackMediator = attackMediator;
            _attackMediator.OnAttackStateChanged += HandleStateChanged;
        }

        protected abstract void HandleStateChanged(AttackState state);
    }
}
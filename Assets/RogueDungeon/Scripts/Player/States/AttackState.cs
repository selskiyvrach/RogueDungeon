namespace RogueDungeon.Player.States
{
    public class AttackState : FinishableByAnimationState<IAttackAnimation>
    {
        public AttackState(IAttackAnimation animation, bool controlAnimation = true) : base(animation, controlAnimation)
        {
        }
    }
}
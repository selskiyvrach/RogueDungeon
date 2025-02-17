using Common.Animations;
using Common.MoveSets;

namespace RogueDungeon.Player.Behaviours.Hands
{
    public class UnsheathMove : HandHeldMove
    {
        public UnsheathMove(IHandheldContext context, MoveConfig config, IAnimator animator) : base(context, config, animator)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Context.CurrentItem = Context.IntendedItem;
            Context.IntendedItem = null;
        }

        public override void Exit()
        {
            base.Exit();
            Context.SetCurrentItemInteractable(true);
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && Context.CurrentItem == null && Context.IntendedItem != null;
            
    }
}
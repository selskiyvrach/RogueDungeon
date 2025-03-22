using Common.Animations;
using Common.MoveSets;

namespace RogueDungeon.Player.Model.Behaviours.Hands
{
    public class SheathMove : HandHeldMove
    {
        public SheathMove(IHandheldContext context, MoveConfig config, IAnimation animation) : base(context, config, animation)
        {
        }
        
        public override void Enter()
        {
            base.Enter();
            Context.SetCurrentItemInteractable(false);
        }

        public override void Exit()
        {
            base.Exit();
            Context.CurrentItem = null;
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && Context.CurrentItem != null && Context.CurrentItem != Context.IntendedItem;
    }
}
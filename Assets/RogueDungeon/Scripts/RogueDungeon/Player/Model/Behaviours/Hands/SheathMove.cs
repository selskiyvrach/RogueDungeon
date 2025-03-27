using Common.Animations;
using Common.MoveSets;

namespace RogueDungeon.Player.Model.Behaviours.Hands
{
    public class SheathMove : HandsState
    {
        private readonly PlayerHandBehaviour _handBehaviour;

        public SheathMove(PlayerHandBehaviour handBehaviour, MoveConfig config, IAnimation animation) : base(config, animation) => 
            _handBehaviour = handBehaviour;

        public override void Enter()
        {
            base.Enter();
            _handBehaviour.SetItemMoveSetActive(false);
        }

        public override void Exit()
        {
            base.Exit();
            _handBehaviour.CurrentItem = null;
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && _handBehaviour.CurrentItem != null && _handBehaviour.CurrentItem != _handBehaviour.IntendedItem;
    }
}
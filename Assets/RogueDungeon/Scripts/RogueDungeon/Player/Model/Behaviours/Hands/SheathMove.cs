using Common.Animations;
using Common.MoveSets;

namespace RogueDungeon.Player.Model.Behaviours.Hands
{
    public class SheathMove : HandsState
    {
        private readonly PlayerHandsBehaviour _hands;
        private readonly PlayerHandBehaviour _handBehaviour;

        public SheathMove(PlayerHandBehaviour handBehaviour, IAnimation animation, PlayerHandsBehaviour hands,
            HandHeldMoveConfig config) : base(config, animation)
        {
            _handBehaviour = handBehaviour;
            _hands = hands;
        }

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
            base.CanTransitionTo() && _hands.OppositeHand(_handBehaviour).IsIdle && _handBehaviour.CurrentItem != null && _handBehaviour.CurrentItem != _handBehaviour.IntendedItem;
    }
}
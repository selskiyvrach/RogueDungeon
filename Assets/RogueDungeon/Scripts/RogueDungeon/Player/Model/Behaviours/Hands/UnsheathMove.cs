using Common.Animations;
using Common.MoveSets;

namespace RogueDungeon.Player.Model.Behaviours.Hands
{
    public class UnsheathMove : HandsState
    {
        private readonly PlayerHandBehaviour _handBehaviour;

        public UnsheathMove(PlayerHandBehaviour handBehaviour, MoveConfig config, IAnimation animation) : base(config, animation) => 
            _handBehaviour = handBehaviour;

        public override void Enter()
        {
            base.Enter();
            _handBehaviour.CurrentItem = _handBehaviour.IntendedItem;
        }

        public override void Exit()
        {
            base.Exit();
            _handBehaviour.SetItemMoveSetActive(true);
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && _handBehaviour.CurrentItem == null && _handBehaviour.IntendedItem != null;
            
    }
}
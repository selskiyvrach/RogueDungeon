using Common.Animations;
using Common.MoveSets;
using RogueDungeon.Items;

namespace RogueDungeon.Player.Model.Behaviours.Hands
{
    public class SheathMove : PlayerMove
    {
        private readonly IItem _item;
        private readonly PlayerHandsBehaviour _hands;
        private readonly PlayerHandBehaviour _handBehaviour;
        protected override float Duration => _item.Config.UnsheathDuration;
        
        public SheathMove(PlayerHandBehaviour handBehaviour, IAnimation animation, PlayerHandsBehaviour hands, string id, IItem item) : base(id, animation)
        {
            _handBehaviour = handBehaviour;
            _hands = hands;
            _item = item;
        }

        public override void Tick(float timeDelta)
        {
            base.Tick(timeDelta);
            if(IsFinished && _handBehaviour.CurrentItem == _item)
                _handBehaviour.CurrentItem = null;
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() /* && _hands.OppositeHand(_handBehaviour).IsIdle */ && _handBehaviour.CurrentItem != null && _handBehaviour.CurrentItem != _handBehaviour.IntendedItem;
    }
}
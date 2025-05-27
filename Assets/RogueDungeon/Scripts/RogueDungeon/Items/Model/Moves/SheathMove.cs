using Common.Animations;

namespace RogueDungeon.Items.Model.Moves
{
    public class SheathMove : PlayerMove
    {
        private readonly IItem _item;
        private readonly IItemSwapper _swapper;
        protected override float Duration => _item.Config.UnsheathDuration;
        
        public SheathMove(IItemSwapper swapper, IAnimation animation, string id, IItem item) : base(id, animation)
        {
            _swapper = swapper;
            _item = item;
        }

        public override void Tick(float timeDelta)
        {
            base.Tick(timeDelta);
            if(IsFinished && _swapper.CurrentItem == _item)
                _swapper.CurrentItem = null;
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && _swapper.CurrentItem != null && _swapper.CurrentItem != _swapper.IntendedItem;
    }
}
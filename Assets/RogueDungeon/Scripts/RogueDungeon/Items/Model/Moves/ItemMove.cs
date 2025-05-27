using Common.Animations;
using Input;
using Moves;

namespace RogueDungeon.Items.Model.Moves
{
    public abstract class ItemMove : PlayerInputMove
    {
        private readonly IItemTransitionsLockedProvider _wielder;
        
        // both none indicate to not apply input logic in base class
        protected override InputKey RequiredKey => InputKey.None;
        protected override RequiredState State => RequiredState.None;

        protected ItemMove(string id, IAnimation animation, IItemTransitionsLockedProvider transitionsLockedProvider, IPlayerInput input) : base(id, animation, input) => 
            _wielder = transitionsLockedProvider;
        
        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && !_wielder.ItemTransitionsAreLocked;

    }
}
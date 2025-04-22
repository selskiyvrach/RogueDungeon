using Common.Animations;
using RogueDungeon.Input;
using RogueDungeon.Player.Model.Behaviours;
using RogueDungeon.Player.Model.Behaviours.Hands;

namespace RogueDungeon.Player.Model.Attacks
{
    public abstract class ItemMove : PlayerInputMove
    {
        private readonly PlayerHandsBehaviour _hands;
        
        // both none indicate to not apply input logic in base class
        protected override InputKey RequiredKey => InputKey.None;
        protected override RequiredState State => RequiredState.None;

        protected ItemMove(string id, IAnimation animation, PlayerHandsBehaviour hands, IPlayerInput input) : base(id, animation, input) => 
            _hands = hands;
        
        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && !_hands.TransitionsLocked;

    }
}
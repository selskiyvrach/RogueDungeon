using Common.Animations;
using Common.MoveSets;

namespace RogueDungeon.Player.Behaviours.Hands
{
    public abstract class HandHeldMove : Move
    {
        protected readonly IHandheldContext Context;
        
        protected HandHeldMove(IHandheldContext context, MoveConfig config, IAnimator animator) : base(config, animator) => 
            Context = context;
    }
}
using Common.Animations;
using Common.MoveSets;

namespace RogueDungeon.Player.Behaviours.Hands
{
    public abstract class HandHeldMove : Move
    {
        protected readonly IHandheldContext Context;
        
        protected HandHeldMove(IHandheldContext context, MoveConfig config, IAnimation animation) : base(config, animation) => 
            Context = context;
    }
}
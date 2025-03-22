using Common.Animations;
using Common.MoveSets;

namespace RogueDungeon.Player.Model.Behaviours.Hands
{
    public class HandHeldIdle : HandHeldMove
    {
        public HandHeldIdle(IHandheldContext context, MoveConfig config, IAnimation animation) : base(context, config, animation)
        {
        }
    }
}
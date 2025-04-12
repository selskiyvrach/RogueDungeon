using Common.Animations;
using Common.MoveSets;

namespace RogueDungeon.Player.Model.Behaviours.Hands
{
    public class HandHeldIdle : HandsState
    {
        protected override float Duration => 1;

        public HandHeldIdle(HandHeldMoveConfig config, IAnimation animation) : base(config, animation)
        {
        }
    }
}
using Common.Animations;
using Common.MoveSets;

namespace RogueDungeon.Player.Model.Behaviours.Hands
{
    public abstract class HandsState : Move
    {
        protected HandsState(MoveConfig config, IAnimation animation) : base(config, animation)
        {
        }
    }
}
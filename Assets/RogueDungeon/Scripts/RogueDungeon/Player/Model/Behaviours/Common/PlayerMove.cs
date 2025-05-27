using Common.Animations;
using Common.MoveSets;

namespace Player.Model.Behaviours.Common
{
    public abstract class PlayerMove : Move
    {
        protected PlayerMove(string id, IAnimation animation) : base(id, animation)
        {
        }
    }
}
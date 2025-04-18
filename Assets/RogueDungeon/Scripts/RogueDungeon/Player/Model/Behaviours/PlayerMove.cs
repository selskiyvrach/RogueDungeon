using Common.Animations;
using Common.MoveSets;

namespace RogueDungeon.Player.Model.Behaviours
{ 
    public abstract class PlayerMove : Move
    {
        protected PlayerMove(string id, IAnimation animation) : base(id, animation)
        {
        }
    }
}
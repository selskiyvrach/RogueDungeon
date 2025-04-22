using Common.Animations;
using Common.MoveSets;
using RogueDungeon.Player.Model.Behaviours.Common;

namespace RogueDungeon.Player.Model.Behaviours
{ 
    public abstract class PlayerMove : Move
    {
        protected PlayerMove(string id, IAnimation animation) : base(id, animation)
        {
        }
    }
}
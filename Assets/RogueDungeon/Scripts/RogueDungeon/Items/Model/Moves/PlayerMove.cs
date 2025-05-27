using Common.Animations;
using Common.MoveSets;

namespace RogueDungeon.Items.Model.Moves
{
    public abstract class PlayerMove : Move
    {
        protected PlayerMove(string id, IAnimation animation) : base(id, animation)
        { 
        }
    }
}
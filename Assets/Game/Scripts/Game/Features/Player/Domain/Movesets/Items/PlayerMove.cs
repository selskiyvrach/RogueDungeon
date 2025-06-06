using Libs.Animations;
using Libs.Movesets;

namespace Game.Features.Player.Domain.Movesets.Items
{
    public abstract class PlayerMove : Move
    {
        protected PlayerMove(string id, IAnimation animation) : base(id, animation)
        { 
        }
    }
}
using Libs.Animations;

namespace Game.Features.Player.Domain.Movesets.Movement
{
    public class InventoryKeepOpenMove : PlayerMove
    {
        protected override float Duration { get; }
        
        public InventoryKeepOpenMove(string id, IAnimation animation) : base(id, animation)
        {
        }
    }
}
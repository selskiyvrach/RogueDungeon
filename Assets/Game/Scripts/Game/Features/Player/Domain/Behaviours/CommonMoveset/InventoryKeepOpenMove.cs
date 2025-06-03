using Libs.Animations;

namespace Game.Features.Player.Domain.Behaviours.CommonMoveset
{
    public class InventoryKeepOpenMove : PlayerMove
    {
        protected override float Duration { get; }
        
        public InventoryKeepOpenMove(string id, IAnimation animation) : base(id, animation)
        {
        }
    }
}
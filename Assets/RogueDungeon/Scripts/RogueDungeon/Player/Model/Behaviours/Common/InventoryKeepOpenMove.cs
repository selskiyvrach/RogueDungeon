using Common.Animations;

namespace Player.Model.Behaviours.Common
{
    public class InventoryKeepOpenMove : PlayerMove
    {
        protected override float Duration { get; }
        
        public InventoryKeepOpenMove(string id, IAnimation animation) : base(id, animation)
        {
        }
    }
}
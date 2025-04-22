using Common.Animations;
using RogueDungeon.Input;
using RogueDungeon.Items;
using RogueDungeon.Player.Model.Behaviours.Hands;

namespace RogueDungeon.Player.Model.Attacks
{
    public class ItemAbsorbBlockImpactMove : ItemMove
    {
        private readonly IBlockingItem _item;
        private readonly Player _player;
        protected override float Duration => ((BlockingItemConfig)_item.Config).BlockImpactAbsorptionDuration;

        protected ItemAbsorbBlockImpactMove(IBlockingItem item, IAnimation animation, Player player, string id,
            PlayerHandsBehaviour hands, IPlayerInput input) : base(id, animation, hands, input)
        {
            _item = item;
            _player = player;
        }

        public override void Enter()
        {
            base.Enter();
            _player.HasUnabsorbedBlockImpact = false;
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && _player.HasUnabsorbedBlockImpact;
    }
}
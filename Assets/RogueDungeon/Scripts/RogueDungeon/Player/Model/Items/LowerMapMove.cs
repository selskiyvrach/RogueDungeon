using Common.Animations;
using RogueDungeon.Input;
using RogueDungeon.Player.Model.Behaviours;
using RogueDungeon.Player.Model.Behaviours.Hands;

namespace RogueDungeon.Items
{
    public class LowerMapMove : PlayerMove
    {
        private readonly PlayerHandsBehaviour _hands;
        private readonly IPlayerInput _playerInput;
        private readonly Map _map;
        protected override float Duration => ((MapItemConfig)_map.Config).LowerMapDuration;
        
        public LowerMapMove(string id, Map map, IAnimation animation, IPlayerInput playerInput, PlayerHandsBehaviour hands) : base(id, animation)
        {
            _map = map;
            _playerInput = playerInput;
            _hands = hands;
        }

        public override void Enter()
        {
            _playerInput.GetKey(_hands.UseItemInput(_map)).Reset();
            base.Enter();
        }

        protected override bool CanTransitionTo() =>
            base.CanTransitionTo() && (_playerInput.GetKey(InputKey.Esc).IsDown || _playerInput.GetKey(_hands.UseItemInput(_map)).IsDown);
    }
}
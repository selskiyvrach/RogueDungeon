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
            if(_playerInput.IsDown(_hands.UseItemInput(_map)))
                _playerInput.ConsumeInput(_hands.UseItemInput(_map));
            base.Enter();
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && (_playerInput.IsDown(InputKey.Esc) || _playerInput.IsDown(_hands.UseItemInput(_map)));
    }
}
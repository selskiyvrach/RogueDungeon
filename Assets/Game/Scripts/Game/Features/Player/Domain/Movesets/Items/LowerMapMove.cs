using Game.Features.Player.Domain.Movesets.Items.Interfaces;
using Game.Libs.Input;
using Game.Libs.Items;
using Libs.Animations;

namespace Game.Features.Player.Domain.Movesets.Items
{
    public class LowerMapMove : PlayerMove
    {
        private readonly IItemInputKeyProvider _inputKeyProvider;
        private readonly IPlayerInput _playerInput;
        private readonly Map _map;
        protected override float Duration => _map.LowerMapDuration;
        
        public LowerMapMove(string id, Map map, IAnimation animation, IPlayerInput playerInput, IItemInputKeyProvider inputKeyProvider) : base(id, animation)
        {
            _map = map;
            _playerInput = playerInput;
            _inputKeyProvider = inputKeyProvider;
        }

        public override void Enter()
        {
            _playerInput.GetKey(_inputKeyProvider.GetInputKeyForItem(_map)).Reset();
            base.Enter();
        }

        protected override bool CanTransitionTo() =>
            base.CanTransitionTo() && (_playerInput.GetKey(InputKey.Esc).IsDown || _playerInput.GetKey(_inputKeyProvider.GetInputKeyForItem(_map)).IsDown);
    }
}
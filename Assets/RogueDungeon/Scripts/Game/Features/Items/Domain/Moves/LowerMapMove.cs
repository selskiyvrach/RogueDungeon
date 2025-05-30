using Game.Features.Items.Domain.Configs;
using Game.Features.Items.Domain.Wielder;
using Game.Libs.Input;
using Libs.Animations;

namespace Game.Features.Items.Domain.Moves
{
    public class LowerMapMove : PlayerMove
    {
        private readonly IItemInputKeyProvider _inputKeyProvider;
        private readonly IPlayerInput _playerInput;
        private readonly Map _map;
        protected override float Duration => ((MapItemConfig)_map.Config).LowerMapDuration;
        
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
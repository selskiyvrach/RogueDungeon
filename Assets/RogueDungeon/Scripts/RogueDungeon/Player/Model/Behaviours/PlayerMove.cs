using Common.Animations;
using Common.MoveSets;
using RogueDungeon.Input;

namespace RogueDungeon.Player.Model.Behaviours
{ 
    public abstract class PlayerMove : Move
    {
        protected PlayerMove(string id, IAnimation animation) : base(id, animation)
        {
        }
    }

    public abstract class PlayerInputMove : PlayerMove
    {
        private readonly IPlayerInput _playerInput;

        protected abstract InputKey RequiredKey { get; }

        protected PlayerInputMove(string id, IAnimation animation, IPlayerInput playerInput) : base(id, animation) => 
            _playerInput = playerInput;

        public override void Enter()
        {
            _playerInput.ConsumeInput(RequiredKey);
            base.Enter();
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && _playerInput.HasInput(RequiredKey);
    }
}
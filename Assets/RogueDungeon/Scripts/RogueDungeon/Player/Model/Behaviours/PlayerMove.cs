using Common.Animations;
using Common.MoveSets;
using RogueDungeon.Input;

namespace RogueDungeon.Player.Model.Behaviours
{ 
    public abstract class PlayerMove : Move
    {
        private readonly IPlayerInput _playerInput;
        
        protected virtual InputKey RequiredKey => InputKey.None;

        protected PlayerMove(PlayerMoveConfig config, IAnimation animation, IPlayerInput playerInput) : base(config, animation) => 
            _playerInput = playerInput;

        public override void Enter()
        {
            if(RequiredKey is not InputKey.None)
                _playerInput.ConsumeInput(RequiredKey);
            base.Enter();
        }

        protected override bool CanTransitionTo() => 
            base.CanTransitionTo() && (RequiredKey is InputKey.None || _playerInput.HasInput(RequiredKey));
    }
}
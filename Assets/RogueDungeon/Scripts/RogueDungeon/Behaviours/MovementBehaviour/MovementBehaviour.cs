using Common.Behaviours;
using Common.FSM;

namespace RogueDungeon.Behaviours.MovementBehaviour
{
    public class MovementBehaviour : Behaviour
    {
        private readonly IDodgeMediator _mediator;
        private readonly IMovementAnimationsPlayer _animationsPlayer;
        private readonly IDodgeSoundsPlayer _soundsPlayer;
        private readonly IDodgeTimingsProvider _timingsProvider;
        private readonly IDodgeInputProvider _inputProvider;
        private readonly StateMachine _stateMachine;

        public MovementBehaviour(
            IDodgeMediator mediator, 
            IMovementAnimationsPlayer animationsPlayer, 
            IDodgeSoundsPlayer soundsPlayer, 
            IDodgeTimingsProvider timingsProvider, 
            IDodgeInputProvider inputProvider)
        {
            _mediator = mediator;
            _animationsPlayer = animationsPlayer;
            _soundsPlayer = soundsPlayer;
            _timingsProvider = timingsProvider;
            _inputProvider = inputProvider;
            var dile = new State()
                .OnEnter(() => _mediator.DodgeState = DodgeState.None)
                .OnEnter(() => _animationsPlayer.PlayIdle());
            
            var dodgeRight = new TimerState(_timingsProvider.GetDuration)
                .OnEnter(() => _mediator.DodgeState = DodgeState.Right)
                .OnEnter(() => _animationsPlayer.PlayDodgeRight(_timingsProvider.GetDuration()))
                .OnEnter(_soundsPlayer.PlayDodgeSound);

            var dodgeLeft = new TimerState(_timingsProvider.GetDuration)
                .OnEnter(() => _mediator.DodgeState = DodgeState.Left)
                .OnEnter(() => _animationsPlayer.PlayDodgeLeft(_timingsProvider.GetDuration()))
                .OnEnter(_soundsPlayer.PlayDodgeSound);

            var fsmBuilder = new StateMachineBuilder(dile, dodgeRight, dodgeLeft);
            var canStartDodge = new If(_mediator.CanStartDodge);
            fsmBuilder.AddTransition(dile, dodgeRight, new IfAll(canStartDodge, new If(_inputProvider.HasDodgeRightInput)));
            fsmBuilder.AddTransition(dile, dodgeLeft, new IfAll(canStartDodge, new If(_inputProvider.HasDodgeLeftInput)));
            fsmBuilder.AddTransitionFromFinished(dodgeRight, dile);
            fsmBuilder.AddTransitionFromFinished(dodgeLeft, dile);
            _stateMachine = fsmBuilder.Build();
        }

        public override void Enable()
        {
            base.Enable();
            _mediator.DodgeState = DodgeState.None;
            _stateMachine.Run();
        }

        public override void Disable()
        {
            base.Disable();
            _mediator.DodgeState = DodgeState.None;
            _stateMachine.Stop();
        }

        public override void Tick() => 
            _stateMachine.Tick();
    }
}
using Common.Behaviours;
using Common.FSM;
using Common.Prameters;
using Common.Properties;
using RogueDungeon.Parameters;
using RogueDungeon.Player;
using RogueDungeon.PlayerInputCommands;

namespace RogueDungeon.Behaviours
{
    public class DodgeBehaviour : Behaviour
    {
        private readonly StateMachine _stateMachine;
        protected override ITickable Tickable => _stateMachine;

        public DodgeBehaviour(IProperty<DodgeState> dodge, CharacterControlStateResolver control, Parameters<Timings> parameters, ICharacterInput input)
        {
            var dodgeIdle = new State().OnEnter(() => dodge.Value = DodgeState.None);
            
            var dodgeRight = new TimerState(parameters.Get(Timings.DodgeDuration)).OnEnter(() => dodge.Value = DodgeState.Right);
            var dodgeLeft = new TimerState(parameters.Get(Timings.DodgeDuration)).OnEnter(() => dodge.Value = DodgeState.Left);

            var dodgeBuilder = new StateMachineBuilder(dodgeIdle, dodgeRight, dodgeLeft);
            var canStartDodgeExecution = new If(control.CanStartHardMovementAnim);
            dodgeBuilder.AddTransition(dodgeIdle, dodgeRight, new IfAll(canStartDodgeExecution, new HasCommand(Command.DodgeRight, input)));
            dodgeBuilder.AddTransition(dodgeIdle, dodgeLeft, new IfAll(canStartDodgeExecution, new HasCommand(Command.DodgeLeft, input)));
            dodgeBuilder.AddTransitionFromFinished(dodgeRight, dodgeIdle);
            dodgeBuilder.AddTransitionFromFinished(dodgeLeft, dodgeIdle);
            _stateMachine = dodgeBuilder.Build();
        }
    }
}
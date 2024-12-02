using Common.DotNetUtils;
using Common.FSM;
using Common.Prameters;
using Common.Properties;
using Common.Registries;
using RogueDungeon.Parameters;
using RogueDungeon.PlayerInputCommands;

namespace RogueDungeon.Player
{
    public class DodgeBehaviour
    {
        public DodgeBehaviour(IProperty<DodgeState> dodge, CharacterControlStateResolver control, Parameters<Timings> parameters, ICharacterInput input)
        {
            var dodgeIdle = new State().OnEnter(() => dodge.Value = DodgeState.None);
            
            // problem with that is that dodge duration is not counted with all the affecting parameters
            var dodgeRight = new TimerState(parameters.Get(Timings.DodgeDuration)).OnEnter(() => dodge.Value = DodgeState.Right);
            var dodgeLeft = new TimerState(parameters.Get(Timings.DodgeDuration)).OnEnter(() => dodge.Value = DodgeState.Left);

            var dodgeBuilder = new StateMachineBuilder(dodgeIdle, dodgeRight, dodgeLeft);
            var canStartDodgeExecution = new If(control.CanStartHardMovementAnim);
            dodgeBuilder.AddTransition(dodgeIdle, dodgeRight, new IfAll(canStartDodgeExecution, new HasCommand(Command.DodgeRight, input)));
            dodgeBuilder.AddTransition(dodgeIdle, dodgeLeft, new IfAll(canStartDodgeExecution, new HasCommand(Command.DodgeLeft, input)));
            dodgeBuilder.AddTransitionFromFinished(dodgeRight, dodgeIdle);
            dodgeBuilder.AddTransitionFromFinished(dodgeLeft, dodgeIdle);
        }
    }

    public class AttackBehaviour
    {
        public AttackBehaviour(CharacterControlStateResolver control, IProperty<AttackState> attack, Parameters<Timings> timings, ICharacterInput input)
        {
            var attackIdle = new State().OnEnter(() => attack.Value = AttackState.None);
            var prepareAttack = new TimerState(timings.Get(Timings.AttackPrepareDuration)).OnEnter(() => attack.Value = AttackState.Preparing);
            var finishAttack = new TimerState(timings.Get(Timings.AttackFinishDuration)).OnEnter(() => attack.Value = AttackState.Finishing);
            var executeAttack = new TimerState(timings.Get(Timings.AttackExecuteDuration)).OnEnter(() => attack.Value = AttackState.Executing);
            var attackBuilder = new StateMachineBuilder(attackIdle, prepareAttack, executeAttack, finishAttack);

            var canStartHardAnim = new If(control.CanStartHardHandsAnim);
            var notDodging = new Not(canStartHardAnim);
            attackBuilder.AddTransition(attackIdle, prepareAttack, new IfAll(new If(control.CanStartSoftHandsAnim), new HasCommand(Command.Attack, input)));
            attackBuilder.AddTransitionFromFinished(prepareAttack, executeAttack, notDodging);
            attackBuilder.AddTransitionFromFinished(prepareAttack, finishAttack, canStartHardAnim);
            attackBuilder.AddTransitionFromFinished(executeAttack, finishAttack);
            attackBuilder.AddTransitionFromFinished(finishAttack, attackIdle);
        }
    }
}
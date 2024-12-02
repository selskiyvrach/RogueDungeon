using Common.FSM;
using Common.Prameters;
using Common.Properties;
using RogueDungeon.PlayerInputCommands;

namespace RogueDungeon.Player
{
    public class DodgeDuration : Parameter
    {
        public DodgeDuration(float value, Type paramType = Type.Flat) : base(value, paramType)
        {
        }
    }

    public class DodgeBehaviour
    {
        public DodgeBehaviour(IProperty<DodgeState> dodge, CharacterControlStateResolver control, DodgeDuration dodgeDuration, ICharacterInput input)
        {
            var dodgeIdle = new State().OnEnter(() => dodge.Value = DodgeState.None);
            var dodgeRight = new TimerState(dodgeDuration).OnEnter(() => dodge.Value = DodgeState.Right);
            var dodgeLeft = new TimerState(dodgeDuration).OnEnter(() => dodge.Value = DodgeState.Left);

            var dodgeBuilder = new StateMachineBuilder(dodgeIdle, dodgeRight, dodgeLeft);
            var canStartDodgeExecution = new If(control.CanStartHardMovementAnim);
            dodgeBuilder.AddTransition(dodgeIdle, dodgeRight, new IfAll(canStartDodgeExecution, new HasCommand(Command.DodgeRight, input)));
            dodgeBuilder.AddTransition(dodgeIdle, dodgeLeft, new IfAll(canStartDodgeExecution, new HasCommand(Command.DodgeLeft, input)));
            dodgeBuilder.AddTransitionFromFinished(dodgeRight, dodgeIdle);
            dodgeBuilder.AddTransitionFromFinished(dodgeLeft, dodgeIdle);
        }
    }
    
    public class AttackPrepareDuration : Parameter
    {
        public AttackPrepareDuration(float value, Type paramType = Type.Flat) : base(value, paramType)
        {
        }
    }
    
    public class AttackExecuteDuration : Parameter
    {
        public AttackExecuteDuration(float value, Type paramType = Type.Flat) : base(value, paramType)
        {
        }
    }
    
    public class AttackFinishDuration : Parameter
    {
        public AttackFinishDuration(float value, Type paramType = Type.Flat) : base(value, paramType)
        {
        }
    }

    public class AttackBehaviour
    {
        public AttackBehaviour(CharacterControlStateResolver control, IProperty<AttackState> attack, 
            AttackPrepareDuration prepareDuration, 
            AttackExecuteDuration executeDuration, 
            AttackFinishDuration finishDuration, 
            ICharacterInput input)
        {
            var attackIdle = new State().OnEnter(() => attack.Value = AttackState.None);
            var prepareAttack = new TimerState(prepareDuration).OnEnter(() => attack.Value = AttackState.Preparing);
            var finishAttack = new TimerState(finishDuration).OnEnter(() => attack.Value = AttackState.Finishing);
            var executeAttack = new TimerState(executeDuration).OnEnter(() => attack.Value = AttackState.Executing);
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
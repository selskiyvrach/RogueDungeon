using Common.FSM;
using Common.Prameters;
using Common.Properties;
using RogueDungeon.PlayerInputCommands;

namespace RogueDungeon.Player
{
    public class DodgeDuration : Parameter
    {
    }

    public class DodgeBehaviour
    {
        public DodgeBehaviour(IProperty<DodgeState> dodge, IReadOnlyProperty<AttackState> attack, IReadOnlyProperty<DodgeDuration> dodgeDuration, ICharacterInput input)
        {
            var dodgeIdle = new State().OnEnter(() => dodge.Value = DodgeState.None);
            var dodgeRight = new TimerState(dodgeDuration).OnEnter(() => dodge.Value = DodgeState.Right);
            var dodgeLeft = new TimerState(dodgeDuration).OnEnter(() => dodge.Value = DodgeState.Left);

            var dodgeBuilder = new StateMachineBuilder(dodgeIdle, dodgeRight, dodgeLeft);
            var notAttacking = new If(() => attack.Value != AttackState.Executing);
            dodgeBuilder.AddTransition(dodgeIdle, dodgeRight, new IfAll(notAttacking, new HasCommand(Command.DodgeRight, input)));
            dodgeBuilder.AddTransition(dodgeIdle, dodgeLeft, new IfAll(notAttacking, new HasCommand(Command.DodgeLeft, input)));
            dodgeBuilder.AddTransitionFromFinished(dodgeRight, dodgeIdle);
            dodgeBuilder.AddTransitionFromFinished(dodgeLeft, dodgeIdle);
        }
    }
    
    public class AttackPrepareDuration : Parameter
    {
    }
    
    public class AttackExecuteDuration : Parameter
    {
    }
    
    public class AttackFinishDuration : Parameter
    {
    }

    public class AttackBehaviour
    {
        public AttackBehaviour(IReadOnlyProperty<DodgeState> dodge, IProperty<AttackState> attack, 
            IReadOnlyProperty<AttackPrepareDuration> prepareDuration, 
            IReadOnlyProperty<AttackExecuteDuration> executeDuration, 
            IReadOnlyProperty<AttackFinishDuration> finishDuration, 
            ICharacterInput input)
        {
            var attackIdle = new State().OnEnter(() => attack.Value = AttackState.None);
            var prepareAttack = new TimerState(prepareDuration).OnEnter(() => attack.Value = AttackState.Preparing);
            var finishAttack = new TimerState(finishDuration).OnEnter(() => attack.Value = AttackState.Finishing);
            var executeAttack = new TimerState(executeDuration).OnEnter(() => attack.Value = AttackState.Executing);
            var attackBuilder = new StateMachineBuilder(attackIdle, prepareAttack, executeAttack, finishAttack);

            var dodging = new If(() => dodge.Value != DodgeState.None);
            var notDodging = new Not(dodging);
            attackBuilder.AddTransition(attackIdle, prepareAttack, new IfAll(notDodging, new HasCommand(Command.Attack, input)));
            attackBuilder.AddTransitionFromFinished(prepareAttack, executeAttack, notDodging);
            attackBuilder.AddTransitionFromFinished(prepareAttack, finishAttack, dodging);
            attackBuilder.AddTransitionFromFinished(executeAttack, finishAttack);
            attackBuilder.AddTransitionFromFinished(finishAttack, attackIdle);
        }
    }
}
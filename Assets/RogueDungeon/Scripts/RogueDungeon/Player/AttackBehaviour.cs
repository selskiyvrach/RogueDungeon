using Common.FSM;
using RogueDungeon.Entities.Prameters;
using RogueDungeon.Entities.Properties;
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
        public DodgeBehaviour(IProperty<DodgeState> dodge, IProperty<AttackState> attack, IProperty<DodgeDuration> dodgeDuration, ICharacterInput input)
        {
            var dodgeIdle = new State().OnEnter(() => dodge.Value = DodgeState.None);
            var dodgeRight = new TimerState(1).OnEnter(() => dodge.Value = DodgeState.Right);
            var dodgeLeft = new TimerState(1).OnEnter(() => dodge.Value = DodgeState.Left);

            var dodgeBuilder = new StateMachineBuilder(dodgeIdle, dodgeRight, dodgeLeft);
            var notAttacking = new If(() => attack.Value != AttackState.Executing);
            dodgeBuilder.AddTransition(dodgeIdle, dodgeRight, new IfAll(notAttacking, new HasCommand(Command.DodgeRight, input)));
            dodgeBuilder.AddTransition(dodgeIdle, dodgeLeft, new IfAll(notAttacking, new HasCommand(Command.DodgeLeft, input)));
            dodgeBuilder.AddTransitionFromFinished(dodgeRight, dodgeIdle);
            dodgeBuilder.AddTransitionFromFinished(dodgeLeft, dodgeIdle);
        }
    }
    
    public class AttackBehaviour
    {
        public AttackBehaviour(Property<DodgeState> dodge, Property<AttackState> attack, ICharacterInput input)
        {
            var attackIdle = new State().OnEnter(() => attack.Value = AttackState.None);
            var prepareAttack = new TimerState(.35f).OnEnter(() => attack.Value = AttackState.Preparing);
            var finishAttack = new TimerState(.35f).OnEnter(() => attack.Value = AttackState.Finishing);
            var executeAttack = new TimerState(.25f).OnEnter(() => attack.Value = AttackState.Executing);
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
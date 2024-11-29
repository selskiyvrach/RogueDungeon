using Common.FSM;
using Common.Providers;
using RogueDungeon.PlayerInputCommands;

namespace RogueDungeon.Player
{
    public class PlayerBehaviour
    {
        private readonly IPlayerInput _input;

        public PlayerBehaviour(IPlayerInput input) => 
            _input = input;

        public void Init()
        {
            var attack = new Value<bool>();
            var isAttacking = new ValueCondition(attack);
            var dodge = new Value<bool>();
            var isDodging = new ValueCondition(dodge);
            
            var dodgeIdle = new State();
            var dodgeRight = new TimerState(1).Bind(dodge);
            var dodgeLeft = new TimerState(1).Bind(dodge);

            var dodgeBuilder = new StateMachineBuilder(dodgeIdle, dodgeRight, dodgeLeft);
            dodgeBuilder.AddTransition(dodgeIdle, dodgeRight, new IfAll(new Not(isAttacking), new HasCommand(Command.DodgeRight, _input)));
            dodgeBuilder.AddTransition(dodgeIdle, dodgeLeft, new IfAll(new Not(isAttacking), new HasCommand(Command.DodgeLeft, _input)));
            dodgeBuilder.AddTransitionFromFinished(dodgeRight, dodgeIdle);
            dodgeBuilder.AddTransitionFromFinished(dodgeLeft, dodgeIdle);
            
            var attackIdle = new State();
            var prepareAttack = new TimerState(.35f);
            var finishAttack = new TimerState(.35f);
            var executeAttack = new TimerState(.25f).Bind(attack);
            var attackBuilder = new StateMachineBuilder(attackIdle, prepareAttack, executeAttack, finishAttack);
            attackBuilder.AddTransition(attackIdle, prepareAttack, new IfAll(new Not(isDodging), new HasCommand(Command.Attack, _input)));
            attackBuilder.AddTransitionFromFinished(prepareAttack, executeAttack, new Not(isDodging));
            attackBuilder.AddTransitionFromFinished(prepareAttack, finishAttack, isDodging);
            attackBuilder.AddTransitionFromFinished(executeAttack, finishAttack);
            attackBuilder.AddTransitionFromFinished(finishAttack, attackIdle);
        }
    }
}
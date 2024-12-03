using Common.Behaviours;
using Common.FSM;
using Common.Prameters;
using Common.Properties;
using RogueDungeon.Parameters;
using RogueDungeon.Player;
using RogueDungeon.PlayerInputCommands;

namespace RogueDungeon.Behaviours.AttackBehaviour
{
    public class AttackBehaviour : Behaviour
    {
        private readonly StateMachine _stateMachine;
        protected override ITickable Tickable => _stateMachine;

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
            _stateMachine = attackBuilder.Build();
        }
    }
}
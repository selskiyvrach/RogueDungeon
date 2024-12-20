using Common.Fsm;

namespace RogueDungeon.Player.Behaviours.Dodge
{
    public class DodgeIdleState : IState
    {
        private readonly IDodgeInput _input;
        private readonly ICanDodgeGetter _getter;

        public DodgeIdleState(IDodgeInput input, ICanDodgeGetter getter)
        {
            _input = input;
            _getter = getter;
        }

        public void CheckTransitions(IStateChanger stateChanger)
        {
            if(!_getter.CanDodge)
                return;

            if (_input.TryConsume(DodgeInputCommand.DodgeLeft))
                stateChanger.To<DodgeLeftState>();
            else if(_input.TryConsume(DodgeInputCommand.DodgeRight))
                stateChanger.To<DodgeRightState>();
        }
    }
}
using Common.Fsm;
using RogueDungeon.Characters.Commands;

namespace RogueDungeon.Player.Behaviours.Dodge
{
    public class DodgeIdleState : IState
    {
        private readonly ICharacterCommands _input;
        private readonly ICanDodgeGetter _getter;

        public DodgeIdleState(ICharacterCommands input, ICanDodgeGetter getter)
        {
            _input = input;
            _getter = getter;
        }

        public void CheckTransitions(IStateChanger stateChanger)
        {
            if(!_getter.CanDodge)
                return;

            if (_input.TryConsume<IDodgeLeftCommand>())
                stateChanger.To<DodgeLeftState>();
            else if(_input.TryConsume<IDodgeRightCommand>())
                stateChanger.To<DodgeRightState>();
        }
    }
}
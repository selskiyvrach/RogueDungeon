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

        public void CheckTransitions(ITypeBasedStateChanger typeBasedStateChanger)
        {
            if(!_getter.CanDodge)
                return;

            if (_input.TryConsume<IDodgeLeftCommand>())
                typeBasedStateChanger.ChangeState<DodgeLeftState>();
            else if(_input.TryConsume<IDodgeRightCommand>())
                typeBasedStateChanger.ChangeState<DodgeRightState>();
        }
    }
}
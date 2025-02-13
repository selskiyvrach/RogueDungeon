using Common.Fsm;
using RogueDungeon.Characters.Input;

namespace RogueDungeon.Player.Behaviours.Dodge
{
    public class DodgeIdleState : ITypeBasedTransitionableState
    {
        private readonly ICharacterInput _input;
        private readonly ICanDodgeGetter _getter;

        public DodgeIdleState(ICharacterInput input, ICanDodgeGetter getter)
        {
            _input = input;
            _getter = getter;
        }

        public void CheckTransitions(ITypeBasedStateChanger typeBasedStateChanger)
        {
            if(!_getter.CanDodge)
                return;

            if (_input.TryConsume(Input.DodgeLeft))
                typeBasedStateChanger.ChangeState<DodgeLeftState>();
            else if(_input.TryConsume(Input.DodgeRight))
                typeBasedStateChanger.ChangeState<DodgeRightState>();
        }
    }
}
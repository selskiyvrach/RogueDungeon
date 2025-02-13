using RogueDungeon.Characters.Input;
using RogueDungeon.Player.Behaviours.Dodge;
using Zenject;

namespace RogueDungeon.Player
{
    public class PlayerControlStateMediator : ITickable
    {
        private readonly ICharacterInput _characterInput;
        private readonly IDodgeStateGetter _dodgeStateGetter;
        private readonly ICanDodgeSetter _canDodgeSetter;

        public PlayerControlStateMediator(IDodgeStateGetter dodgeStateGetter, ICanDodgeSetter canDodgeSetter, ICharacterInput characterInput)
        {
            _dodgeStateGetter = dodgeStateGetter;
            _canDodgeSetter = canDodgeSetter;
            _characterInput = characterInput;
        }

        [Inject]
        public void Tick()
        {
            
            _canDodgeSetter.CanDodge = true;
        }
    }
}
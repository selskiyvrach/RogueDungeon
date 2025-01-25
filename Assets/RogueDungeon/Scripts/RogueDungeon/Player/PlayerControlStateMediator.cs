using RogueDungeon.Characters.Commands;
using RogueDungeon.Player.Behaviours.Dodge;
using Zenject;

namespace RogueDungeon.Player
{
    public class PlayerControlStateMediator : ITickable
    {
        private readonly ICharacterCommands _characterCommands;
        private readonly IDodgeStateGetter _dodgeStateGetter;
        private readonly ICanDodgeSetter _canDodgeSetter;

        public PlayerControlStateMediator(IDodgeStateGetter dodgeStateGetter, ICanDodgeSetter canDodgeSetter, ICharacterCommands characterCommands)
        {
            _dodgeStateGetter = dodgeStateGetter;
            _canDodgeSetter = canDodgeSetter;
            _characterCommands = characterCommands;
        }

        [Inject]
        public void Tick()
        {
            
            _canDodgeSetter.CanDodge = true;
        }
    }
}
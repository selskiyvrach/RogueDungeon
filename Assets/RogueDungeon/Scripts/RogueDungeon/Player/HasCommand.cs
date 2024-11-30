using Common.FSM;
using RogueDungeon.PlayerInputCommands;

namespace RogueDungeon.Player
{
    public class HasCommand : ICondition
    {
        private readonly ICharacterInput _characterInput;
        private readonly Command _command;

        public HasCommand(Command command, ICharacterInput characterInput)
        {
            _characterInput = characterInput;
            _command = command;
        }

        public bool IsMet() => 
            _characterInput.HasCommand(_command);
    }
}
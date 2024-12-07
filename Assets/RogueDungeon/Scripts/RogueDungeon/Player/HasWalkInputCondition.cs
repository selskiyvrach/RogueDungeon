using Common.FSM;
using RogueDungeon.PlayerInput;

namespace RogueDungeon.Player
{
    public class HasInputCondition : ICondition
    {
        private readonly ICharacterInput _commandsReader;
        private readonly Command _command;

        public HasInputCondition(ICharacterInput commandsReader, Command command)
        {
            _commandsReader = commandsReader;
            _command = command;
        }

        public bool IsMet() =>
            _commandsReader.HasCommand(_command);
    }
}
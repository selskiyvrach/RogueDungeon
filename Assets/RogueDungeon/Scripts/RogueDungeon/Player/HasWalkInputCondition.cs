using Common.FSM;
using RogueDungeon.PlayerInputCommands;

namespace RogueDungeon.Player
{
    public class HasInputCondition : ICondition
    {
        private readonly ICommandsProvider _commandsReader;
        private readonly Command _command;

        public HasInputCondition(ICommandsProvider commandsReader, Command command)
        {
            _commandsReader = commandsReader;
            _command = command;
        }

        public bool IsMet() =>
            _commandsReader.HasCommand(_command);
    }
}
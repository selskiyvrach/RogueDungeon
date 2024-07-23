using RogueDungeon.Player.Commands;
using RogueDungeon.StateMachine;

namespace RogueDungeon.Player
{
    public class HasInputCondition : ICondition
    {
        private readonly Commands.CommandsReader _commandsReader;
        private readonly Command _command;

        public HasInputCondition(Commands.CommandsReader commandsReader, Command command)
        {
            _commandsReader = commandsReader;
            _command = command;
        }

        public bool IsMet() =>
            _commandsReader.HasCommand(_command);
    }
}
using RogueDungeon.Gameplay.InputCommands;
using RogueDungeon.Services.FSM;

namespace RogueDungeon.Gameplay
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
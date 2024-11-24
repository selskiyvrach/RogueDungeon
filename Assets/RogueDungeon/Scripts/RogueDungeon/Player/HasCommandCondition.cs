using Common.FSM;
using RogueDungeon.PlayerInputCommands;

namespace RogueDungeon.Player
{
    public class HasCommandCondition : ICondition
    {
        private readonly ICommandsProvider _commandsProvider;
        private readonly Command _command;

        public HasCommandCondition(Command command, ICommandsProvider commandsProvider)
        {
            _commandsProvider = commandsProvider;
            _command = command;
        }

        public bool IsMet() => 
            _commandsProvider.HasCommand(_command);
    }
}
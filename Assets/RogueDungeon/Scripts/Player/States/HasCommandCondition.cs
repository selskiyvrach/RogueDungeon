using RogueDungeon.Player.Commands;
using RogueDungeon.StateMachine;

namespace RogueDungeon.Player.States
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
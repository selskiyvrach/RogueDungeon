using RogueDungeon.Gameplay.InputCommands;
using RogueDungeon.Services.FSM;

namespace RogueDungeon.Gameplay.States
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
using RogueDungeon.Player.Commands;
using RogueDungeon.StateMachine;

namespace RogueDungeon.Player
{
    public class HasInputCondition : ICondition
    {
        private readonly Commands.Commands _commands;
        private readonly Command _command;

        public HasInputCondition(Commands.Commands commands, Command command)
        {
            _commands = commands;
            _command = command;
        }

        public bool IsMet() =>
            _commands.HasCommand(_command);
    }
}
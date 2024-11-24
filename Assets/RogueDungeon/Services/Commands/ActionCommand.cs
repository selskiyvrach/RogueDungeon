using System;

namespace RogueDungeon.Services.Commands
{
    public class ActionCommand : Command
    {
        private readonly Action _action;

        public ActionCommand(Action action) => 
            _action = action;

        public override void Execute() => 
            _action?.Invoke();
    }
}
using System;
using UnityEngine;

namespace RogueDungeon.Player.Commands
{
    public class CommandsReader : ICommandsProvider
    {
        public bool HasCommand(Command command) => 
            Input.GetKey(command switch
            {
                Command.Attack => KeyCode.Mouse0,
                Command.Block => KeyCode.Mouse1,
                Command.DodgeLeft => KeyCode.A,
                Command.DodgeRight => KeyCode.D,
                Command.MoveForward => KeyCode.W,
                _ => throw new ArgumentOutOfRangeException(nameof(command), command, null)
            });
    }
}
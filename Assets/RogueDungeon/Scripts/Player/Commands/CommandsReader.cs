using System.Collections.Generic;
using UnityEngine;

namespace RogueDungeon.Player.Commands
{
    public class CommandsReader : MonoBehaviour, ICommandsProvider, ICommandsConsumer
    {
        private readonly Dictionary<Command, KeyCode> _commandCodes = new ()
        {
            [Command.MoveForward] = KeyCode.W,
            [Command.Attack] = KeyCode.Mouse0,
            [Command.DodgeLeft] = KeyCode.A,
            [Command.DodgeRight] = KeyCode.D,
            [Command.Block] = KeyCode.Mouse1,
        };

        private Command? _currentCommand;

        private void Update()
        {
            foreach (var (command, keyCode) in _commandCodes)
            {
                if (Input.GetKey(keyCode))
                    _currentCommand = command;
            }
        }

        public bool HasCommand(Command command) => 
            _currentCommand == command;

        public void ConsumeCommandIfCurrent(Command command, bool logError = true)
        {
            if(logError && _currentCommand != command)
                Debug.LogError($"Trying to consume an invalid command. current: {_currentCommand}, consuming: {command}");
            if (_currentCommand == command)
                _currentCommand = null;
        }
    }
}
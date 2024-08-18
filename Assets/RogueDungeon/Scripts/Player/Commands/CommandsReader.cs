using System.Collections.Generic;
using UnityEngine;

namespace RogueDungeon.Player.Commands
{
    public class CommandsReader : MonoBehaviour, ICommandsProvider, ICommandsConsumer
    {
        private readonly Dictionary<Command, KeyCode> _commandCodes = new ()
        {
            [Command.Attack] = KeyCode.Mouse0,
            [Command.DodgeLeft] = KeyCode.A,
            [Command.DodgeRight] = KeyCode.D,
        };

        private Command? _currentCommand;

        private void Update()
        {
            if (_currentCommand == Command.MoveForward)
                _currentCommand = null;
            if (Input.GetKey(KeyCode.W))
                _currentCommand = Command.MoveForward;
            
            foreach (var (command, keyCode) in _commandCodes)
            {
                if (Input.GetKeyDown(keyCode))
                    _currentCommand = command;
            }

            if (Input.GetKey(KeyCode.Mouse1))
                _currentCommand = Command.Block;
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
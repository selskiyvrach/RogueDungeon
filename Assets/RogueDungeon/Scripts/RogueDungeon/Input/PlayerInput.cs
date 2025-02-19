using System;
using System.Collections.Generic;
using Common.UtilsDotNet;
using UniRx;
using UnityEngine;

namespace RogueDungeon.Input
{
    public class PlayerInput : IDisposable, IPlayerInput
    {
        private enum KeyState
        {
            Down,
            Held
        }
        
        private static readonly Dictionary<InputKey, (KeyCode keyCode, KeyState state, float coyoteTime)> Commands = new()
        {
            // [Input.MoveForward] = (KeyCode.W,KeyState.Held , 0),
            [InputKey.DodgeRight] = (KeyCode.D, KeyState.Down,.5f),
            [InputKey.DodgeLeft] = (KeyCode.A, KeyState.Down,.5f),
            [InputKey.LightAttack] = (KeyCode.Mouse0, KeyState.Down,.5f),
            // [Input.Block] = (KeyCode.Mouse1, KeyState.Held,.5f),
        };

        private readonly IDisposable _sub;
        private InputKey _currentInputKey;
        private float _timeSinceReleased;
        private float _timeHeld;

        public PlayerInput() => 
            _sub = Observable.EveryUpdate().Subscribe(_ => Tick());

        public void Dispose() => 
            _sub?.Dispose();

        private void Tick()
        {
            UpdateCoyoteTime();
            ReadCommands();
        }

        public bool HasInput(InputKey inputKey) => 
            _currentInputKey == inputKey.ThrowIfNone();

        public void ConsumeInput(InputKey inputKey)
        {
            if (!HasInput(inputKey))
                throw new InvalidOperationException($"Cannot consume command of a wrong type. Current: {_currentInputKey}, consuming: {inputKey}");
            
            _currentInputKey = InputKey.None;
            _timeHeld = 0;
            _timeSinceReleased = Mathf.Infinity;
        }

        private void ReadCommands()
        {
            var currentCommand = InputKey.None;

            foreach (var (command, (code, state, coyoteTime)) in Commands)
            {
                if (! (state switch
                    {
                        KeyState.Down => UnityEngine.Input.GetKeyDown(code),
                        KeyState.Held => UnityEngine.Input.GetKey(code),
                        _ => throw new ArgumentOutOfRangeException()
                    } )) 
                    continue;
                
                _timeSinceReleased = 0;
                if (currentCommand != command)
                {
                    currentCommand = command;
                    _currentInputKey = currentCommand;
                    _timeHeld = 0;
                }
                else
                    _timeHeld += Time.deltaTime;
            }
        }

        private void UpdateCoyoteTime()
        {
            if (_currentInputKey == InputKey.None) 
                return;
            
            _timeSinceReleased += Time.deltaTime;
            if (_timeSinceReleased >= Commands[_currentInputKey].coyoteTime) 
                _currentInputKey = InputKey.None;
        }
    }
}
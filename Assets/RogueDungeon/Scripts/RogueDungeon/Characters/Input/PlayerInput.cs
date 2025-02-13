using System;
using System.Collections.Generic;
using Common.UtilsDotNet;
using UniRx;
using UnityEngine;

namespace RogueDungeon.Characters.Input
{
    public class PlayerInput : IDisposable, ICharacterInput
    {
        private enum KeyState
        {
            Down,
            Held
        }
        
        private static readonly Dictionary<Input, (KeyCode keyCode, KeyState state, float coyoteTime)> Commands = new()
        {
            // [Input.MoveForward] = (KeyCode.W,KeyState.Held , 0),
            [Input.DodgeRight] = (KeyCode.D, KeyState.Down,.5f),
            [Input.DodgeLeft] = (KeyCode.A, KeyState.Down,.5f),
            // [Input.Block] = (KeyCode.Mouse1, KeyState.Held,.5f),
        };

        private readonly IDisposable _sub;
        private Input _currentInput;
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

        public bool HasInput(Input input) => 
            _currentInput == input.ThrowIfNone();

        public void ConsumeInput(Input input)
        {
            if (!HasInput(input))
                throw new InvalidOperationException($"Cannot consume command of a wrong type. Current: {_currentInput}, consuming: {input}");
            
            _currentInput = Input.None;
            _timeHeld = 0;
            _timeSinceReleased = Mathf.Infinity;
        }

        private void ReadCommands()
        {
            var currentCommand = Input.None;

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
                    _currentInput = currentCommand;
                    _timeHeld = 0;
                }
                else
                    _timeHeld += Time.deltaTime;
            }
        }

        private void UpdateCoyoteTime()
        {
            if (_currentInput == Input.None) 
                return;
            
            _timeSinceReleased += Time.deltaTime;
            if (_timeSinceReleased >= Commands[_currentInput].coyoteTime) 
                _currentInput = Input.None;
        }
    }
}
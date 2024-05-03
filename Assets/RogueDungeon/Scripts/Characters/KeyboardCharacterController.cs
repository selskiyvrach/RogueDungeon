using System.Collections.Generic;
using RogueDungeon.Actions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;

namespace RogueDungeon.Characters
{
    public class KeyboardCharacterController : CharacterController
    {
        private readonly PlayerCharacterConfig _config;
        private readonly Dictionary<string, Action> _actions;
        
        private string _pendingCommand;
        private int _coyoteTimeFrames;

        public KeyboardCharacterController(Character character) : base(character)
        {
            _config = character.Config as PlayerCharacterConfig;
            Assert.IsNotNull(_config);
            _actions = new Dictionary<string, Action>
            {
                ["RaiseBlock"] = new BlockAction(_config.UnarmedBlock),
                ["DodgeLeft"] = new DodgeAction(_config.DodgeLeft, DodgeState.DodgingLeft), 
                ["DodgeRight"] = new DodgeAction(_config.DodgeRight, DodgeState.DodgingRight),
                ["Attack"] = new AttackAction(_config.UnarmedAttack),
            };
        }

        public override void Tick()
        {
            if(Input.GetKeyDown(KeyCode.A))
                RegisterInputCommand("DodgeLeft");
            if(Input.GetKeyDown(KeyCode.D))
                RegisterInputCommand("DodgeRight");
            if(Input.GetKeyDown(KeyCode.Mouse0))
                RegisterInputCommand("Attack");
            if(Input.GetKeyDown(KeyCode.Mouse1))
                RegisterInputCommand("RaiseBlock");
            if(Input.GetKeyUp(KeyCode.Mouse1))
                RegisterInputCommand("LowerBlock");
            
            base.Tick();
            
            if(_pendingCommand == null)
                return;
            
            if (_pendingCommand == "LowerBlock" && CurrentAction is BlockAction block)
            {
                block.OnCommand("LowerBlock");
                _pendingCommand = null;
            }
            
            if (CurrentAction == null)
            {
                if (_pendingCommand == "RaiseBlockThenLower")
                {
                    StartAction(_actions["RaiseBlock"]);
                    CurrentAction.OnCommand("LowerBlock");
                }
                else
                    StartAction(_actions[_pendingCommand]);

                _pendingCommand = null;
                return;
            }

            if (_coyoteTimeFrames-- == 0)
                _pendingCommand = null;
        }

        private void RegisterInputCommand(string command)
        {
            if (_pendingCommand == "RaiseBlock" && command == "LowerBlock")
                _pendingCommand = "RaiseBlockThenLower";
            else
                _pendingCommand = command;
            _coyoteTimeFrames = 15;
        }
    }
}
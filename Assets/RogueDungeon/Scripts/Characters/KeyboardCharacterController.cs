using System.Collections.Generic;
using RogueDungeon.Actions;
using UnityEngine.Assertions;

namespace RogueDungeon.Characters
{
    public class KeyboardCharacterController : CharacterController
    {
        private readonly PlayerCharacterConfig _config;
        private readonly Dictionary<string, Action> _actions;
        
        private string _pendingCommand;
        private int _coyoteTimeFrames;
        private bool _startedHandlingDeath;

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
                ["Idle"] = new IdleAction(_config.IdleAction),
            };
        }

        public override void Tick()
        {
            if (Character.Health.IsDead && !_startedHandlingDeath)
            {
                StartAction(new DeathAction(_config.DeathActionConfig));
                _startedHandlingDeath = true;
            }

            if (CurrentAction is DeathAction)
            {
                base.Tick();
                return;
            }
            
            if(_startedHandlingDeath)
                return;

            if(Input.Input.GetUnit(Input.Action.DodgeLeft).Down)
                RegisterInputCommand("DodgeLeft");
            if(Input.Input.GetUnit(Input.Action.DodgeRight).Down)
                RegisterInputCommand("DodgeRight");
            if(Input.Input.GetUnit(Input.Action.Attack).Down)
                RegisterInputCommand("Attack");
            if(Input.Input.GetUnit(Input.Action.Block).Held)
                RegisterInputCommand("RaiseBlock");
            
            base.Tick();

            if (CurrentAction is BlockAction block && _pendingCommand != "RaiseBlock") 
                block.OnCommand("LowerBlock");

            if(_pendingCommand == null)
                return;

            if (_pendingCommand is "RaiseBlock" or "DodgeLeft" or "DodgeRight")
                if (CurrentAction is AttackAction)
                {
                    ResetAnimation();
                    StopCurrentAction();
                }
            
            if (CurrentAction == null)
            {
                StartAction(_actions[_pendingCommand]);
                _pendingCommand = null;
                return;
            }

            if (_coyoteTimeFrames-- == 0)
                _pendingCommand = null;
        }

        private void RegisterInputCommand(string command)
        {
            _pendingCommand = command;
            _coyoteTimeFrames = 15;
            if (CurrentAction is BlockAction && _pendingCommand == "RaiseBlock")
                _coyoteTimeFrames = 1;
        }
    }
}
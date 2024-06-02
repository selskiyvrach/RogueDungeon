using System.Collections.Generic;
using RogueDungeon.Actions;
using RogueDungeon.Items;
using UnityEngine.Assertions;

namespace RogueDungeon.Characters
{
    public class PlayerCharacterController : CharacterController
    {
        private readonly PlayerCharacterConfig _config;
        private readonly Dictionary<string, Action> _actions;
        
        private string _pendingCommand;
        private bool _startedHandlingDeath;
        private readonly Combo _attackCombo;
        
        public PlayerCharacterController(Character character) : base(character)
        {
            _config = character.Config as PlayerCharacterConfig;
            Assert.IsNotNull(_config);
            _attackCombo = new Combo(_config.Attack);
            _actions = new Dictionary<string, Action>
            {
                ["RaiseBlock"] = new BlockAction(_config.UnarmedBlock),
                ["DodgeLeft"] = new DodgeAction(_config.DodgeLeft, DodgeState.DodgingLeft), 
                ["DodgeRight"] = new DodgeAction(_config.DodgeRight, DodgeState.DodgingRight),
                ["Attack"] = new AttackAction(_attackCombo),
                ["Idle"] = new IdleAction(_config.IdleAction),
            };
        }

        public override void Tick()
        {
            if (Character.Health.IsDepleted && !_startedHandlingDeath)
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
            if(Input.Input.GetUnit(Input.Action.Block).Down)
                RegisterInputCommand("RaiseBlock");
            if(Input.Input.GetUnit(Input.Action.Block).Up)
                RegisterInputCommand("LowerBlock");
            
            base.Tick();

            if (_pendingCommand == "LowerBlock")
            {
                if(CurrentAction is BlockAction block) 
                    block.OnCommand("LowerBlock");
                _pendingCommand = null;
            }
            
            if(CurrentAction is IdleAction && _pendingCommand != null)
                StopCurrentAction();

            if (CurrentAction != null) 
                return;
            if (_pendingCommand == "ContinueCombo")
            {
                _attackCombo.CurrentIndex++;
                _attackCombo.CurrentIndex %= _attackCombo.Length;
                _pendingCommand = "Attack";
            }
            StartAction(_actions[_pendingCommand ?? "Idle"]);
            _pendingCommand = null;
        }

        private void RegisterInputCommand(string command)
        {
            _pendingCommand = command;
            if (_pendingCommand == "Attack") 
            {
                if (CurrentAction is AttackAction)
                    _pendingCommand = "ContinueCombo";
                else
                    _attackCombo.CurrentIndex = 0;
            }
        }
    }
}
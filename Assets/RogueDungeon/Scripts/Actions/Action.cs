using System;
using System.Linq;
using RogueDungeon.Characters;

namespace RogueDungeon.Actions
{
    public abstract class Action
    {
        private readonly ActionConfig _config;
        private bool _hasStarted;
        private bool _isRewinding;

        public virtual bool IsFinished => _hasStarted && (!IsRewinding && CurrentFrame == _config.Frames || IsRewinding && CurrentFrame == 1);

        public int CurrentFrame { get; private set; }
        public int Frames => _config.Frames;
        public string AnimationName => _config.AnimationName;
        public string Command => _config.Command;

        protected bool IsRewinding
        {
            get => _isRewinding;
            set
            {
                _isRewinding = value;
                TryRaiseCallback();
            }
        }

        protected Action(ActionConfig config) => 
            _config = config;

        public void Start()
        {
            _hasStarted = true;
            _isRewinding = false;
            CurrentFrame = 1;
            OnStarted();
            TryRaiseCallback();
        }

        public void Tick()
        {
            switch (IsRewinding)
            {
                case false when CurrentFrame == _config.Frames:
                case true when CurrentFrame == 1:
                    return;
            }

            CurrentFrame += IsRewinding ? -1 : 1;
            TryRaiseCallback();
        }

        public void Stop()
        {
            _hasStarted = false;
            OnStop();
        }

        public virtual void OnCommand(string command)
        {
        }

        private void TryRaiseCallback()
        {
            var keyframe = _config.GetKeyframe(CurrentFrame); 
            if(keyframe != null)
                OnKeyframe(keyframe);
        }

        protected abstract void OnKeyframe(string keyframe);

        protected virtual void OnStarted()
        {
        }

        protected virtual void OnStop()
        {
        }
    }

    public static class ActionFactory
    {
        public static Action Create(Character character, string name) =>
            name switch
            {
                "UnarmedAttack" => new AttackAction(character, character.Config.AttackConfigs.First(n => n.Id is "UnarmedAttack").Config, DodgeState.NotDodging),
                "UnarmedBlock" => new BlockAction(character, character.Config.BlockConfigs.First(n => n.Id == "UnarmedBlock").Config),
                "DodgeLeft" => new DodgeAction(character, DodgeState.DodgingLeft, character.Config.ActionConfigs.First(n => n.Name == "DodgeLeft")),
                "DodgeRight" => new DodgeAction(character, DodgeState.DodgingRight, character.Config.ActionConfigs.First(n => n.Name == "DodgeRight")),
                "AttackCenter" => new AttackAction(character, character.Config.AttackConfigs.First(n => n.Id == "AttackCenter").Config, DodgeState.NotDodging),            
                "AttackLeft" => new AttackAction(character, character.Config.AttackConfigs.First(n => n.Id == "AttackLeft").Config, DodgeState.DodgingRight),
                "AttackRight" => new AttackAction(character, character.Config.AttackConfigs.First(n => n.Id == "AttackRight").Config, DodgeState.DodgingLeft),
                _ => throw new ArgumentOutOfRangeException(nameof(name), name, null),
            };
    }
}
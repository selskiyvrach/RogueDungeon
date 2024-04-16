using System.Collections.Generic;
using RogueDungeon.Actions;
using RogueDungeon.Animations;
using RogueDungeon.Health;
using RogueDungeon.Stats;

namespace RogueDungeon.Characters
{
    public class Character : IStatsProvider
    {
        private readonly Animator _animator;
        public string Id { get; }
        public CharacterConfig Config { get; }
        public Dictionary<string, Action> Actions { get; } = new();
        public Action CurrentAction { get; private set; }
        public Health.Health Health { get; }
        public CombatState CombatState { get; } = new();

        public Character(CharacterConfig config, Animator animator)
        {
            _animator = animator;
            Config = config;
            Id = Config.Id;
            var hpAmount = GetStat(Constants.HP);
            Health = new Health.Health();
            Health.SetHealth(hpAmount, hpAmount, HealthChangeReason.Recalculated);
        }

        public float GetStat(string id) => 
            Config.GetStat(id) + 
            (CombatState.BlockIsRaised 
                ? CombatState.BlockingWeaponStats.GetStat(id) 
                : 0);

        public void Tick()
        {
            if(CurrentAction == null)
                return;
            CurrentAction.Tick();
            _animator.UpdateState((float)CurrentAction.CurrentFrame / CurrentAction.Frames);
            if (!CurrentAction.IsFinished)
                return;
            // Debug.Log(Actions.FirstOrDefault(n => n.Value == CurrentAction).Key + " finished");
            CurrentAction.Stop();
            CurrentAction = null;
            _animator.SetState(null);
        }

        /// <summary>
        /// Returns false if command could not execute at the moment 
        /// </summary>
        public bool OnCommand(string command)
        {
            if (CurrentAction?.IsFinished ?? false)
                CurrentAction = null;

            if (CurrentAction != null)
            {
                // TODO: find out if this behaviour is ok or should it also be a boolean 
                // for now it returns false no matter the action's ability to handle the command
                // so the command will become a coyote time command in any case
                CurrentAction.OnCommand(command);
                return false;
            }

            if (!Actions.TryGetValue(command, out var action))
                return true;

            CurrentAction = action;
            CurrentAction.Start();
            _animator.SetState(CurrentAction.AnimationName);
            _animator.UpdateState(0);
            return true;
            // Debug.Log(command + " started");
        }
    }
}
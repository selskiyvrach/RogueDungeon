using System.Collections.Generic;
using System.Linq;
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

        public void OnCommand(string command)
        {
            switch (command)
            {
                case "RaiseBlock":
                    if (CurrentAction is BlockAction block1)
                    {
                        block1.OnRaiseBlockCommand();
                        return;
                    }
                    command = "Block";
                    break;
                case "LowerBlock":
                    if (CurrentAction is BlockAction block)
                    {
                        block.OnLowerBlockCommand();
                        return;
                    }
                    command = "Block";
                    break;
            }
            
            if (CurrentAction?.IsFinished ?? false)
                CurrentAction = null;

            if(CurrentAction != null)
                return;
            CurrentAction = Actions[command];
            CurrentAction.Start();
            _animator.SetState(CurrentAction.AnimationName);
            _animator.UpdateState(0);
            // Debug.Log(command + " started");
        }
    }
}
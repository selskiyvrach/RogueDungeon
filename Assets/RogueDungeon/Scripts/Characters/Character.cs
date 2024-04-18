using System.Collections.Generic;
using RogueDungeon.Actions;
using RogueDungeon.Animations;
using RogueDungeon.Health;
using RogueDungeon.Stats;

namespace RogueDungeon.Characters
{
    public class Character : IStatsProvider
    {
        public string Id { get; }
        public CharacterConfig Config { get; }
        public Animator Animator { get; }
        public Dictionary<string, Action> Actions { get; } = new();
        public Action CurrentAction { get; set; }
        public Health.Health Health { get; }
        public CombatState CombatState { get; } = new();
        public CharacterController Controller { get; set; }

        public Character(CharacterConfig config, Animator animator)
        {
            Animator = animator;
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

        public void Tick() => 
            Controller?.Tick();
    }
}
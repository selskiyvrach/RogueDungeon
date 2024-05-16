using RogueDungeon.Data.Stats;
using RogueDungeon.Health;
using RogueDungeon.UI;
using UnityEngine;
using Animator = RogueDungeon.Animations.Animator;

namespace RogueDungeon.Characters
{
    public class Character : IStatsProvider
    {
        public string Id { get; }
        public CharacterConfig Config { get; }
        public Animator Animator { get; }
        public Health.Health Health { get; }
        public IHealthDisplay HealthDisplay { get; }
        public GameObject GameObject { get; }
        public CombatState CombatState { get; } = new();
        public CharacterController Controller { get; set; }

        public Character(CharacterConfig config, Animator animator, IHealthDisplay healthDisplay, GameObject gameObject)
        {
            Animator = animator;
            HealthDisplay = healthDisplay;
            GameObject = gameObject;
            Config = config;
            Id = Config.Id;
            Health = new Health.Health();
        }
        
        public float GetStat(string id) => 
            Config.GetStat(id) + 
            (CombatState.BlockIsRaised 
                ? CombatState.BlockingWeaponStats.GetStat(id) 
                : 0);
    }
}
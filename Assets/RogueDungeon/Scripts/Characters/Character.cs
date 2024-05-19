using RogueDungeon.CharacterResource;
using RogueDungeon.Data.Stats;
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
        public Resource Health { get; }
        public Resource Balance { get; }
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
            Health = new Resource();
            Health.Set(GetStat(Constants.HP), ResourceChangeReason.Recalculated);
            
            Balance = new Resource();
            Balance.Set(GetStat(Constants.BALANCE), ResourceChangeReason.Recalculated);
        }
        
        public float GetStat(string id) => 
            Config.GetStat(id) + 
            (CombatState.BlockIsRaised 
                ? CombatState.BlockingWeaponStats.GetStat(id) 
                : 0);

        public void TakeDamage(float damage, float balanceDamage)
        {
            Health.Spend(damage);
            Balance.Spend(balanceDamage);
        }
    }
}
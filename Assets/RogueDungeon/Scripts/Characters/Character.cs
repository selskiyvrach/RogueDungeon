using JetBrains.Annotations;
using RogueDungeon.CharacterResource;
using RogueDungeon.Data.Stats;
using RogueDungeon.UI;
using UnityEngine;
using UnityEngine.Assertions;
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
        public IResourceDisplay HealthDisplay { get; }
        public GameObject GameObject { get; }
        public CombatState CombatState { get; } = new();
        public CharacterController Controller { get; set; }
        [CanBeNull] public Resource Stamina { get; }
        [CanBeNull] public IResourceDisplay StaminaDisplay { get; }

        public Character(CharacterConfig config, Animator animator, GameObject gameObject, IResourceDisplay healthDisplay, [CanBeNull] IResourceDisplay staminaDisplay = null)
        {
            Animator = animator;
            HealthDisplay = healthDisplay;
            GameObject = gameObject;
            Config = config;
            Id = Config.Id;
            Health = new Resource();
            Health.Set(GetStat(Constants.HP), ResourceChangeReason.Recalculated);

            if (Config.HasStamina)
            {
                Stamina = new Resource();
                Stamina.Set(GetStat("Stamina"), GetStat("Stamina"), ResourceChangeReason.Recalculated);
                StaminaDisplay = staminaDisplay;
                Assert.IsNotNull(staminaDisplay);
            }
            
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
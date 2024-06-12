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
        [CanBeNull] public Resource Stamina { get; set; }
        [CanBeNull] public IResourceDisplay StaminaDisplay { get; set; }

        public Character(CharacterConfig config, Animator animator, GameObject gameObject, IResourceDisplay healthDisplay)
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
            Config.GetStat(id);

        public void TakeDamage(float damage, float balanceDamage)
        {
            Health.Spend(damage);
            Balance.Spend(balanceDamage);
        }
    }
}
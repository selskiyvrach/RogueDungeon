using RogueDungeon.Health;
using RogueDungeon.Stats;
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
        public HealthDisplay HealthDisplay { get; }
        public GameObject GameObject { get; }
        public CombatState CombatState { get; } = new();
        public CharacterActionsController ActionsController { get; set; }

        public Character(CharacterConfig config, Animator animator, HealthDisplay healthDisplay, GameObject gameObject)
        {
            Animator = animator;
            HealthDisplay = healthDisplay;
            GameObject = gameObject;
            Config = config;
            Id = Config.Id;
            
            var hpAmount = GetStat(Constants.HP);
            Health = new Health.Health();
            Health.SetHealth(hpAmount, hpAmount, HealthChangeReason.Recalculated);
            Health.OnChanged += reason => HealthDisplay.HandleHealthChanged(Health, reason);
            Health.OnChanged += reason => Debug.Log($"{Id} health {Health.Current}/{Health.Max}");
        }
        
        public float GetStat(string id) => 
            Config.GetStat(id) + 
            (CombatState.BlockIsRaised 
                ? CombatState.BlockingWeaponStats.GetStat(id) 
                : 0);

        public void Tick()
        {
            ActionsController?.Tick();
            GameObject.transform.position = CombatState.Surroundings.GetWorldCoordinatesForPosition(CombatState.positions);
        }
    }
    
    // player behaviour
        // unarmed attacks
        // unarmed block
        // dodge left
        // dodge right
        // attack -> current weapon
        // block -> current weapon
        
    // enemy behaviour
        // attack patterns[]
        // attack pattern
            // attack[]
            // suitable for positions
            // chill time
        // attack
            // left/right/center
            
    // action executioner
    
    // hivemind
        // 
}
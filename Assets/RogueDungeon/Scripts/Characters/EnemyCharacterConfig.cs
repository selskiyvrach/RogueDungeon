using System.Linq;
using RogueDungeon.Actions;
using RogueDungeon.Items;
using UnityEngine;
using UnityEngine.Assertions;

namespace RogueDungeon.Characters
{
    [CreateAssetMenu(menuName = "Configs/Characters/EnemyCharacter", fileName = "EnemyCharacter", order = 0)]
    public class EnemyCharacterConfig : CharacterConfig
    {
        [field: SerializeField] public EnemyCharacterConfig Extends { get; private set; }
        [field: SerializeField] public AttackPattern[] AttackPatterns { get; private set; }
        [field: SerializeField] public AttackConfigsBank AttackConfigsBank { get; private set; }

        public AttackConfig GetAttackConfig(string id)
        {
            var attackConfig = AttackConfigsBank.TryGetValue(id, out var attack)
                ? attack
                : Extends.GetAttackConfig(id);
            Assert.IsNotNull(attackConfig, $"Attack config with id '{id}' is missing on character config with id'{Id}'");
            return attackConfig;
        }

        public override CharacterController CreateController(Character character, ActionFactory actionFactory)
        {
            var patterns = Extends == null 
                ? AttackPatterns 
                : Extends.AttackPatterns.Concat(AttackPatterns);
            
            return new AiCharacterController(character, actionFactory, patterns);
        }
    }
}
using System.Linq;
using RogueDungeon.Actions;
using RogueDungeon.Items;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

namespace RogueDungeon.Characters
{
    [CreateAssetMenu(menuName = "Configs/Characters/EnemyCharacter", fileName = "EnemyCharacter", order = 0)]
    public class EnemyCharacterConfig : CharacterConfig
    {
        [SerializeField] private EnemyCharacterConfig _extends;
        [SerializeField] private HandyAttackConfig[] _attackConfigs;
        [SerializeField] private AttackPattern[] _attackPatterns;
        
        public IAttackConfig GetAttackConfig(string id)
        {
            var attackConfig = _attackConfigs.FirstOrDefault(n => n.Id == id) ?? _extends.GetAttackConfig(id);
            Assert.IsNotNull(attackConfig, $"Attack config with id '{id}' is missing on character config with id'{Id}'");
            return attackConfig;
        }

        public override CharacterController CreateController(Character character)
        {
            var patterns = _extends == null 
                ? _attackPatterns 
                : _extends._attackPatterns.Concat(_attackPatterns);
            
            return new AiCharacterController(character, patterns);
        }
    }
}
using System;
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
        [Serializable]
        private class AttackConfigById
        {
            public string Id;
            public AttackConfig Attack;
        }
        [SerializeField] private AttackConfigById[] _attackConfigs;
        
        [field: SerializeField] public EnemyCharacterConfig Extends { get; private set; }
        [field: SerializeField] public AttackPattern[] AttackPatters { get; private set; }

        public AttackConfig GetAttackConfig(string id)
        {
            var attack = _attackConfigs.FirstOrDefault(n => n.Id == id)?.Attack;
            if(attack == null && Extends != null)
                attack = Extends.GetAttackConfig(id);
            Assert.IsNotNull(attack, $"Attack config with id '{id}' is missing on character config with id'{Id}'");
            return attack;
        }

        public override CharacterController CreateController(Character character, ActionFactory actionFactory)
        {
            var patterns = Extends == null 
                ? AttackPatters 
                : Extends.AttackPatters.Concat(AttackPatters);
            
            return new AiCharacterController(character, actionFactory, patterns);
        }
    }
}
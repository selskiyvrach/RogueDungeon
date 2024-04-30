using UnityEngine;

namespace RogueDungeon.Characters
{
    [CreateAssetMenu(menuName = "Configs/Characters/EnemyCharacter", fileName = "EnemyCharacter", order = 0)]
    public class EnemyCharacterConfig : CharacterConfig
    {
        [field: SerializeField] public AttackPattern[] AttackPatters { get; private set; }
        
        public override CharacterActionsController CreateController(Character character) => 
            new PatternCharacterActionsController(character, AttackPatters);
    }
}
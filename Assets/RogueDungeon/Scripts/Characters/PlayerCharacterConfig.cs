using RogueDungeon.Actions;
using RogueDungeon.Items;
using UnityEngine;

namespace RogueDungeon.Characters
{
    [CreateAssetMenu(menuName = "Configs/Characters/PlayerCharacterConfig", fileName = "PlayerCharacterConfig", order = 0)]
    public class PlayerCharacterConfig : CharacterConfig
    {
        [field: SerializeField] public ActionConfig DodgeRight  { get; private set; }
        [field: SerializeField] public ActionConfig DodgeLeft  { get; private set; }
        [field: SerializeField] public AttackConfig UnarmedAttack  { get; private set; }
        [field: SerializeField] public BlockConfig UnarmedBlock  { get; private set; }
        
        public override CharacterController CreateController(Character character) => 
            new KeyboardCharacterController(character);
    }
}
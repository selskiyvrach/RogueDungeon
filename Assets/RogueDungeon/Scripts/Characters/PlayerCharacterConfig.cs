using RogueDungeon.Actions;
using RogueDungeon.Items;
using UnityEngine;

namespace RogueDungeon.Characters
{
    [CreateAssetMenu(menuName = "Configs/Characters/PlayerCharacterConfig", fileName = "PlayerCharacterConfig", order = 0)]
    public class PlayerCharacterConfig : CharacterConfig
    {
        [field: SerializeField] public ActionConfig IdleAction  { get; private set; }
        [field: SerializeField] public ActionConfig DodgeRight  { get; private set; }
        [field: SerializeField] public ActionConfig DodgeLeft  { get; private set; }
        [field: SerializeField] public BlockConfig UnarmedBlock  { get; private set; }
        [field: SerializeField] public AttackComboConfig Attack  { get; private set; }
        
        public override CharacterController CreateController(Character character) => 
            new PlayerCharacterController(character);
    }
}
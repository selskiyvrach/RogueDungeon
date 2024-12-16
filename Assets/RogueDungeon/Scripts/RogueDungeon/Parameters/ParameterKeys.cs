using Common.Keys;

namespace RogueDungeon.Parameters
{
    public class ParameterKeys
    {
        public static readonly Key NONE = Key.NONE;
        /// <summary>
        /// Idle to attack and attack to idle transitions, sheath/unsheath
        /// </summary>
        public static readonly Key COMMON_ITEM_MANIPULATION_DURATION = new(nameof(COMMON_ITEM_MANIPULATION_DURATION));
        public static readonly Key IDLE_ANIMATION_SPEED = new(nameof(IDLE_ANIMATION_SPEED));
        
        public static readonly Key ATTACK_TO_ATTACK_TRANSITION_DURATION = new(nameof(ATTACK_TO_ATTACK_TRANSITION_DURATION));
        public static readonly Key ATTACK_EXECUTION_DURATION = new(nameof(ATTACK_EXECUTION_DURATION));
    }
}
using Common.Keys;

namespace RogueDungeon.Parameters
{
    public class ParameterKeys
    {
        public static readonly Key NONE = Key.NONE;
        public static readonly Key ATTACK_TO_ATTACK_TRANSITION_DURATION = new(nameof(ATTACK_TO_ATTACK_TRANSITION_DURATION), 1000);
        public static readonly Key ATTACK_IDLE_TRANSITION_DURATION = new(nameof(ATTACK_IDLE_TRANSITION_DURATION), 1001);
        public static readonly Key ATTACK_EXECUTION_DURATION = new(nameof(ATTACK_EXECUTION_DURATION), 1002);
        public static readonly Key IDLE_ANIMATION_SPEED = new(nameof(IDLE_ANIMATION_SPEED), 1003);

        public static readonly Key UNSHEATH_RIGHT_HAND_DURATION = new(nameof(UNSHEATH_RIGHT_HAND_DURATION), 2000);
        public static readonly Key SHEATH_RIGHT_HAND_DURATION = new(nameof(SHEATH_RIGHT_HAND_DURATION), 2001);
    }
}
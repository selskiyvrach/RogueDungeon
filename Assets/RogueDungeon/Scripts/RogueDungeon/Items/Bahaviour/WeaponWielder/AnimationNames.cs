using Common.Animations;

namespace RogueDungeon.Items.Bahaviour.WeaponWielder
{
    [AnimatorParameters]
    public static class AnimationNames
    {
        public const string HIT_EVENT = "hit";
        
        public const string IDLE = "idle";
        public const string OFF_SCREEN = "off_screen";
        
        public const string ATTACK_PREPARE_TO_BOTTOM_LEFT = "attack_prepare_to_bottom_left";
        public const string ATTACK_TO_BOTTOM_LEFT = "attack_to_bottom_left";
        public const string ATTACK_TO_BOTTOM_RIGHT = "attack_to_bottom_right";
        public const string ATTACK_FINISH_FROM_BOTTOM_RIGHT = "attack_finish_from_bottom_right";
        public const string ATTACK_FINISH_FROM_BOTTOM_LEFT = "attack_finish_from_bottom_left";
        public const string UNSHEATH_RIGHT_HAND = "unsheath_right_hand";
        public const string SHEATH_RIGHT_HAND = "sheath_right_hand";
    }
}
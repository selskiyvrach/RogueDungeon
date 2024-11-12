using RogueDungeon.Animations;
using UnityEngine;

namespace RogueDungeon.Gameplay
{
    [CreateAssetMenu(menuName = "Configs/Player/AnimationsConfig", fileName = "PlayerAnimationsConfig")]
    public class PlayerAnimationsConfig : ScriptableObject
    {
        [SerializeField] private AnimationConfig _walkAnimation;
        [SerializeField] private AnimationConfig _idleAnimation;
        [SerializeField] private AnimationConfig _dodgeRightAnimation;
        [SerializeField] private AnimationConfig _dodgeLeftAnimation;

        public AnimationConfig WalkAnimation => _walkAnimation;
        public AnimationConfig IdleAnimation => _idleAnimation;
        public AnimationConfig DodgeRightAnimation => _dodgeRightAnimation;
        public AnimationConfig DodgeLeftAnimation => _dodgeLeftAnimation;
    }
}
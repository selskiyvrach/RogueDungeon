using RogueDungeon.StateMachine;
using UnityEngine;

namespace RogueDungeon.Player.States
{
    public abstract class WalkAnimationKeyframesHandler : IStateEnterHandler, IStateExitHandler
    {
        private readonly IWalkAnimation _walkAnimation;

        protected WalkAnimationKeyframesHandler(IWalkAnimation walkAnimation) => 
            _walkAnimation = walkAnimation;

        public void OnEnter() => 
            _walkAnimation.OnStepped += HandleStep;

        public void OnExit() => 
            _walkAnimation.OnStepped -= HandleStep;

        protected abstract void HandleStep();
    }

    public class WalkAnimationKeyframesCameraEffectsHandler : WalkAnimationKeyframesHandler
    {
        // ICameraEffectsPlayer
        
        public WalkAnimationKeyframesCameraEffectsHandler(IWalkAnimation walkAnimation) : base(walkAnimation)
        {
        }

        protected override void HandleStep()
        {
            
        }
    }

    public class WalkAnimationKeyframesSoundHandler : WalkAnimationKeyframesHandler
    {
        // ISoundPlayer
        // ISurfaceTypeDetector
        
        public WalkAnimationKeyframesSoundHandler(IWalkAnimation walkAnimation) : base(walkAnimation)
        {
        }

        protected override void HandleStep()
        {
            Debug.Log("On step keyframe");
        }
    }
}
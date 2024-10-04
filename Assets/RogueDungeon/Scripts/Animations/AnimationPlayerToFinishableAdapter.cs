using System;
using RogueDungeon.DebugTools;
using RogueDungeon.StateMachine;

namespace RogueDungeon.Animations
{
    [Serializable]
    public class AnimationPlayerToFinishableAdapter : IFinishable
    {
        private AnimationPlayer _animationPlayer;
        public bool IsFinished => _animationPlayer.IsFinished;

        public AnimationPlayerToFinishableAdapter(AnimationPlayer animationPlayer)
        {
            _animationPlayer = animationPlayer;
            if (_animationPlayer.IsLooped) 
                Logger.LogError(this, "Animation should not be looped in order to be used in this context");
        }
    }
}
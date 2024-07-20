using System;

namespace RogueDungeon.Player
{
    public class WalkAnimationPlayer : AnimationPlayer, IWalkAnimation
    {
        public event Action OnStepped;

        // animation callback
        private void RaiseOnStepped() => 
            OnStepped?.Invoke();
    }
}
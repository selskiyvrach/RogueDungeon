using System;

namespace RogueDungeon.Player
{
    public class WalkAnimationPlayer : AnimationPlayer, IWalkAnimation
    {
        public event Action OnStepped;
        
        protected override void OnEvent(int eventIndex)
        {
            if (eventIndex != 0)
                throw new Exception($"Invalid event index {eventIndex} in {nameof(WalkAnimationPlayer)}");
            
            OnStepped?.Invoke();
        }
    }
}
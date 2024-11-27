using RogueDungeon.Animations;

namespace RogueDungeon.Player
{
    public struct DodgeEvent : IAnimationEvent
    {
        public readonly DodgeState State;

        public DodgeEvent(DodgeState state) => 
            State = state;
    }
}
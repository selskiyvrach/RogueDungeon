using RogueDungeon.Animations;
using RogueDungeon.Characters;
using RogueDungeon.Gameplay;

namespace RogueDungeon.Player
{
    public struct DodgeEvent : IAnimationEvent
    {
        public enum DodgeState
        {
            Started,
            Ended
        }

        public enum DodgeDirection
        {
            Left,
            Right
        }

        public readonly DodgeState State;
        public readonly DodgeDirection Direction;

        public DodgeEvent(DodgeState state, DodgeDirection direction)
        {
            State = state;
            Direction = direction;
        }

        public PlayerDodgeState ToDodgeState() =>
            State == DodgeState.Ended
                ? PlayerDodgeState.None
                : Direction == DodgeDirection.Left
                    ? PlayerDodgeState.Left
                    : PlayerDodgeState.Right;
    }
}
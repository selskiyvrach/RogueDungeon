using RogueDungeon.Animations;

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

        public Player.DodgeState ToDodgeState() =>
            State == DodgeState.Ended
                ? Player.DodgeState.None
                : Direction == DodgeDirection.Left
                    ? Player.DodgeState.Left
                    : Player.DodgeState.Right;
    }
}
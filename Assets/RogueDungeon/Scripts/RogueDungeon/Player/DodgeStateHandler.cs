using System;
using RogueDungeon.Collisions;

namespace RogueDungeon.Player
{
    public class DodgeStateHandler : IDodger
    {
        public Positions Position { get; private set; }

        public void StartDodge(DodgeEvent.DodgeDirection dodgeDirection) =>
            Position = dodgeDirection switch
            {
                DodgeEvent.DodgeDirection.Left => Positions.PlayerDodgeLeft,
                DodgeEvent.DodgeDirection.Right => Positions.PlayerDodgeRight,
                _ => throw new ArgumentOutOfRangeException(nameof(dodgeDirection), dodgeDirection, null)
            };

        public void FinishDodge() => 
            Position = Positions.PlayerDefault;
    }
}
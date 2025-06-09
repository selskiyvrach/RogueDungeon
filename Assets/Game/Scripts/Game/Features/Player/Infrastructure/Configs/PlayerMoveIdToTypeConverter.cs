using System;
using Game.Features.Player.Domain.Movesets.Movement;
using Libs.Movesets;

namespace Game.Features.Player.Domain.Behaviours.CommonMoveset
{
    public class PlayerMoveIdToTypeConverter : IMoveIdToTypeConverter
    {
        public Type GetMoveType(string id) =>
            id switch
            {
                MoveNames.IDLE => typeof(IdleMove),
                MoveNames.BIRTH => typeof(BirthMove),
                MoveNames.DEATH => typeof(DeathMove),
                MoveNames.DODGE_LEFT => typeof(DodgeLeftMove),
                MoveNames.DODGE_RIGHT => typeof(DodgeRightMove),
                MoveNames.MOVE_FORWARD => typeof(MoveForwardMove),
                MoveNames.TURN_LEFT => typeof(TurnLeftMove),
                MoveNames.TURN_RIGHT => typeof(TurnRightMove),
                MoveNames.TURN_AROUND => typeof(TurnAroundMove),
                MoveNames.INVENTORY_OPEN => typeof(InventoryOpenMove),
                MoveNames.INVENTORY_KEEP_OPEN => typeof(InventoryKeepOpenMove),
                MoveNames.INVENTORY_CLOSE => typeof(InventoryCloseMove),
                _ => throw new ArgumentException($"Unknown move id: {id}", nameof(id))
            };
    }
}
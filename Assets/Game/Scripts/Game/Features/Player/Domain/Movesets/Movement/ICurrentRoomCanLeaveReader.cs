namespace Game.Features.Player.Domain.Movesets.Movement
{
    public interface ICurrentRoomCanLeaveReader
    {
        bool CanLeave { get; }
    }
}
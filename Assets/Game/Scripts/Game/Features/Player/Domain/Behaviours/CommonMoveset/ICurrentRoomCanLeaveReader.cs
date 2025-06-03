namespace Game.Features.Player.Domain.Behaviours.CommonMoveset
{
    public interface ICurrentRoomCanLeaveReader
    {
        bool CanLeave { get; }
    }
}
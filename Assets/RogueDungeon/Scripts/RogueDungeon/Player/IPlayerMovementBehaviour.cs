using Common.Behaviours;
using Common.Unity;

namespace RogueDungeon.Player
{
    public interface IPlayerMovementBehaviour : IBehaviour
    {
        TwoDWorldObject ObjectToMove { get; set; }
    }
}
using System.Collections.Generic;

namespace Game.Features.Levels.Domain
{
    public interface ILevelConfig
    {
        IEnumerable<IRoomConfig> Rooms { get; }
    }
}
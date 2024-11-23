using System.Collections.Generic;

namespace RogueDungeon.Collisions
{
    public interface ICollisionsDetector
    {
        IEnumerable<Collision> GetCollisions(Positions positionsMask);
    }
}
using System.Collections.Generic;
using System.Linq;
using Common.DotNetUtils;
using Common.Registries;
using RogueDungeon.Entities;

namespace RogueDungeon.Collisions
{
    public class CollisionsDetector : ICollisionsDetector
    {
        private readonly IRegistry<IRootEntity> _gameEntities;

        public CollisionsDetector(IRegistry<IRootEntity> gameEntities) =>
            _gameEntities = gameEntities;

        public IEnumerable<Collision> GetCollisions(Positions positionsMask) => 
            _gameEntities.GetAll<ICollidable>(n => (n.Position & positionsMask) != 0).Select(n => new Collision(n, n.Position));
    }
}
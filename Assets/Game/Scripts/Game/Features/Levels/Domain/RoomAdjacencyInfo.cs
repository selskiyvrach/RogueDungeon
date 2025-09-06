using System;
using System.Collections.Generic;

namespace Game.Features.Levels.Domain
{
    public readonly struct RoomAdjacencyInfo
    {
        public AdjacencyType Type { get; }
        public float Rotation { get; }

        public RoomAdjacencyInfo(List<NeighborDirection> neighbors) : this()
        {
            var hasTopNeighbor = neighbors.Contains(NeighborDirection.Top);
            var hasBottomNeighbor = neighbors.Contains(NeighborDirection.Bottom);
            var hasLeftNeighbor = neighbors.Contains(NeighborDirection.Left);
            var hasRightNeighbor = neighbors.Contains(NeighborDirection.Right);
            
            Type = neighbors.Count switch
            {
                1 => AdjacencyType.DeadEnd,
                2 when (hasTopNeighbor && hasBottomNeighbor) || (hasLeftNeighbor && hasRightNeighbor) => AdjacencyType.Corridor,
                2 => AdjacencyType.LJoint,
                3 => AdjacencyType.TJoint,
                4 => AdjacencyType.XJoint,
                _ => throw new ArgumentOutOfRangeException()
            };

            Rotation = Type switch
            {
                AdjacencyType.Corridor => hasRightNeighbor ? 90 : 0,
                AdjacencyType.DeadEnd => 
                    hasBottomNeighbor ? 0
                    : hasLeftNeighbor? 90
                    : hasTopNeighbor ? 180
                    : hasRightNeighbor ? 270 
                    : throw new Exception(), 
                AdjacencyType.LJoint => (hasTopNeighbor, hasRightNeighbor, hasBottomNeighbor, hasLeftNeighbor) switch
                {
                    (true,  true,  false, false) => 0,  
                    (false, true,  true,  false) => 90, 
                    (false, false, true,  true ) => 180,
                    (true,  false, false, true ) => 270,
                    _ => throw new Exception(),
                },
                AdjacencyType.TJoint => 
                    !hasTopNeighbor ? 0
                    : !hasRightNeighbor ? 90
                    : !hasBottomNeighbor ? 180
                    : !hasLeftNeighbor ? 270
                    : throw new Exception(),
                AdjacencyType.XJoint => 0,
                _ => throw new Exception(),
            };
        }
    }
}
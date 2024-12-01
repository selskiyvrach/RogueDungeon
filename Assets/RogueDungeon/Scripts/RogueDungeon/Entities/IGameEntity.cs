using RogueDungeon.Collisions;
using UnityEngine;

namespace RogueDungeon.Entities
{
    public interface IGameEntity
    {
        Transform RootTransform { get; }
        Positions Position { get; }
    }
}
using System;
using Common.Unity;
using RogueDungeon.Levels;
using UnityEngine;

namespace RogueDungeon.Player.Behaviours.Movement
{
    public static class Vector2Extensions
    {
        public static Adjacency ToAdjacency(this Vector2Int vector) => 
            vector == Vector2Int.up 
                ? Adjacency.Top
                : vector == Vector2Int.down 
                    ? Adjacency.Bottom 
                    : vector == Vector2Int.right 
                        ? Adjacency.Right 
                        : vector == Vector2Int.left 
                            ? Adjacency.Left
                            : throw new ArgumentException(vector.ToString());
        
        public static Adjacency ToAdjacency(this Vector2 vector) => ToAdjacency(vector.Round()); 
    }
}
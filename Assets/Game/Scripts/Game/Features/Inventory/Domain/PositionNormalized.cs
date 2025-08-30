using System;
using UnityEngine;

namespace Game.Features.Inventory.Domain
{
    public struct PositionNormalized
    {
        public float X {get; private set;}
        public float Y {get; private set;}
        public static PositionNormalized Center { get; } = new(0.5f, 0.5f);
        public static PositionNormalized Zero { get; } = new(0f, 0f);
        public static PositionNormalized One { get; } = new(1f, 1f);

        public PositionNormalized(float x, float y)
        {
            if(x < 0 || x > 1 || y < 0 || y > 1)
                throw new ArgumentOutOfRangeException();
            X = x;
            Y = y;
        }

        public Vector2 ToVector2() => 
            new(X, Y);
        
        public static PositionNormalized FromVector2(Vector2 vector) => 
            new(vector.x, vector.y);

        public PositionNormalized FlipY()
        {
            Y = 1 - Y;
            return this;
        }

        public override string ToString() => 
            $"({X}, {Y})";
    }
}
using System;

namespace Enemies
{
    [Flags]
    public enum EnemyPosition
    {
        None = 0,
        Middle = 1,
        Left = 2,
        Right = 4,
        All = Middle | Left | Right,
    }
}
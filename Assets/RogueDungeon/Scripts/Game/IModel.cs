using System;

namespace RogueDungeon.Game
{
    public interface IModel : IDisposable
    {
        event Action OnDisposed;
    }
}
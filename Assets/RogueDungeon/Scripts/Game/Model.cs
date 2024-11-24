using System;

namespace RogueDungeon.Game
{
    public abstract class Model : IModel
    {
        public event Action OnDisposed;

        public virtual void Dispose() => 
            OnDisposed?.Invoke();
    }
}
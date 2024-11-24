using System;

namespace RogueDungeon.UI.Common
{
    public interface IView : IDisposable 
    {
    }

    public interface IView<in T> : IView, IInitializable<T> where T : IViewModel 
    {
        
    }
}
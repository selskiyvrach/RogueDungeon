using System;
using RogueDungeon.Game;

namespace RogueDungeon.UI.Common
{
    public interface IViewModel : IDisposable
    {
    }

    public interface IViewModel<T> : IViewModel where T : IModel
    {
    }
}
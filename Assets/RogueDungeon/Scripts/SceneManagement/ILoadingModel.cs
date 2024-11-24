using System;
using RogueDungeon.Game;
using UniRx;

namespace RogueDungeon.SceneManagement
{
    public interface ILoadingModel : IModel
    {
        event Action OnFinished;
        IReadOnlyReactiveProperty<float> Progress { get; }
    }
}
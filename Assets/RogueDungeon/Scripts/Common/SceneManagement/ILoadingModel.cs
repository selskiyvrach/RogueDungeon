using System;
using Common.Mvvm.Model;
using UniRx;

namespace Common.SceneManagement
{
    public interface ILoadingModel : IModel
    {
        event Action OnFinished;
        IReadOnlyReactiveProperty<float> Progress { get; }
    }
}
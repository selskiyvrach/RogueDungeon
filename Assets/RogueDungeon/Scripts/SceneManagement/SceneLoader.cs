using System;
using System.Collections;
using RogueDungeon.UI.LoadingScreen;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace RogueDungeon.SceneManagement
{
    public class SceneLoader : ILoadingScreenViewModel, ISceneLoader
    {
        private readonly IFactory<ILoadingScreenViewModel, ILoadingScreenView> _loadingScreenFactory;
        private readonly ReactiveProperty<float> _progress = new();
        private readonly ReactiveProperty<bool> _isFinished = new();

        public IReadOnlyReactiveProperty<float> Progress => _progress;
        public IReadOnlyReactiveProperty<bool> IsFinished => _isFinished;

        public SceneLoader(IFactory<ILoadingScreenViewModel, ILoadingScreenView> loadingScreenFactory) => 
            _loadingScreenFactory = loadingScreenFactory;

        public void LoadScene<T>(Action callback = null) where T : Scene, new()
        {
            _isFinished.Value = false;
            _loadingScreenFactory.Create(this);
            CoroutineRunner.Run(LoadingCoroutine(new T().SceneName, callback));
        }

        private IEnumerator LoadingCoroutine(string sceneName, Action onSceneLoaded)
        {
            var asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            asyncOperation.allowSceneActivation = false;
            asyncOperation.completed += _ =>
            {
                asyncOperation.allowSceneActivation = true;
            };
            while (!asyncOperation.isDone)
            {
                var progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
                _progress.Value = progress;
                yield return null;
            }

            _isFinished.Value = true;
            onSceneLoaded?.Invoke();
        }
    }
}
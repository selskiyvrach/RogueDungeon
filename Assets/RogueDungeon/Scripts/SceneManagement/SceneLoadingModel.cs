using System;
using System.Collections;
using RogueDungeon.Game;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RogueDungeon.SceneManagement
{
    public class SceneLoadingModel : Model, ILoadingModel
    {
        private readonly ReactiveProperty<float> _progress;
        private Coroutine _loadingRoutine;
        public event Action OnFinished;
        public IReadOnlyReactiveProperty<float> Progress => _progress;

        public void Load(string name)
        {
            if (_loadingRoutine != null)
                throw new Exception("Loading process is already running");
            _loadingRoutine = CoroutineRunner.Run(LoadingCoroutine(name));
        }
        
        private IEnumerator LoadingCoroutine(string sceneName)
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

            OnFinished?.Invoke();
        }
    }
}
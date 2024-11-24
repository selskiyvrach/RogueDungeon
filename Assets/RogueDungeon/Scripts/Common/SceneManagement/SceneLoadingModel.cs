using System;
using System.Collections;
using Common.Mvvm.Model;
using Common.UnityUtils;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common.SceneManagement
{
    public class SceneLoadingModel : Model, ISceneLoadingModel
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly ReactiveProperty<float> _progress = new();
        public event Action OnFinished;
        public IReadOnlyReactiveProperty<float> Progress => _progress;

        public SceneLoadingModel(ICoroutineRunner coroutineRunner) => 
            _coroutineRunner = coroutineRunner;

        public void Load(string name)=> 
            _coroutineRunner.Run(LoadingCoroutine(name));

        private IEnumerator LoadingCoroutine(string sceneName)
        {
            var asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            asyncOperation.allowSceneActivation = false;

            while (asyncOperation.progress < 0.9f)
            {
                var progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
                _progress.Value = progress;
                yield return null;
            }

            _progress.Value = 1f; 
            yield return new WaitForSeconds(.2f);

            asyncOperation.allowSceneActivation = true;

            while (!asyncOperation.isDone)
            {
                yield return null;
            }

            OnFinished?.Invoke();
        }
    }
}
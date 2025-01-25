using System;
using System.Collections;
using System.Threading.Tasks;
using Common.Unity;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common.SceneManagement
{
    public class SceneLoader : ISceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private Coroutine _loadingRoutine;

        public SceneLoader(ICoroutineRunner coroutineRunner) => 
            _coroutineRunner = coroutineRunner;

        public async Task Load<T>() where T : Scene, new()
        {
            if (_loadingRoutine != null)
                throw new InvalidOperationException("Another scene loading is already in progress");
            
            _loadingRoutine = _coroutineRunner.Run(LoadingCoroutine(new T().SceneName));
            while (_loadingRoutine != null) 
                await Task.Yield();
        }

        private IEnumerator LoadingCoroutine(string sceneName)
        {
            var asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            asyncOperation.allowSceneActivation = false;

            while (asyncOperation.progress < 0.9f)
                yield return null;

            asyncOperation.allowSceneActivation = true;

            while (!asyncOperation.isDone)
                yield return null;
            
            _loadingRoutine = null;
        }
    }
}
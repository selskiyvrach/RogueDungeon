using System.Collections;
using UnityEngine;

namespace Common.Unity
{
    public interface ICoroutineRunner
    {
        Coroutine Run(IEnumerator coroutine);
        void Stop(Coroutine coroutine);
        void Stop(IEnumerator coroutine);
    }
}
using System.Collections;
using UnityEngine;

namespace Common.UtilsUnity
{
    public interface ICoroutineRunner
    {
        Coroutine Run(IEnumerator coroutine);
        void Stop(Coroutine coroutine);
        void Stop(IEnumerator coroutine);
    }
}
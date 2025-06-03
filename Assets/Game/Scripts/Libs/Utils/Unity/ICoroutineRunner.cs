using System.Collections;
using UnityEngine;

namespace Libs.Utils.Unity
{
    public interface ICoroutineRunner
    {
        Coroutine Run(IEnumerator coroutine);
        void Stop(Coroutine coroutine);
        void Stop(IEnumerator coroutine);
    }
}
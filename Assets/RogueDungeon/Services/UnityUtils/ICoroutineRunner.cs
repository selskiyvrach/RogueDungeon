using System.Collections;
using UnityEngine;

namespace RogueDungeon.Services.UnityUtils
{
    public interface ICoroutineRunner
    {
        Coroutine Run(IEnumerator coroutine);
        void Stop(Coroutine coroutine);
        void Stop(IEnumerator coroutine);
    }
}
using System.Collections;
using UnityEngine;

namespace Common.UtilsUnity
{
    public class CoroutineRunner : MonoBehaviour, ICoroutineRunner
    {
        public Coroutine Run(IEnumerator coroutine) => 
            StartCoroutine(coroutine);

        public void Stop(Coroutine coroutine)
        {
            if (coroutine != null) 
                StopCoroutine(coroutine);
        }

        public void Stop(IEnumerator coroutine)
        {
            if (coroutine != null) 
                StopCoroutine(coroutine);
        }
    }
}
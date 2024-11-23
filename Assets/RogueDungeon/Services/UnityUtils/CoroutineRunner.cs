using UnityEngine;
using System.Collections;

public class CoroutineRunner : MonoBehaviour
{
    private static CoroutineRunner _instance;

    private static CoroutineRunner Instance
    {
        get
        {
            if (_instance != null) 
                return _instance;
            
            var gameObject = new GameObject("CoroutineRunner");
            _instance = gameObject.AddComponent<CoroutineRunner>();
            DontDestroyOnLoad(gameObject);
            return _instance;
        }
    }

    public static Coroutine Run(IEnumerator coroutine) => 
        Instance.StartCoroutine(coroutine);

    public static void Stop(Coroutine coroutine)
    {
        if (coroutine != null) 
            Instance.StopCoroutine(coroutine);
    }

    public static void Stop(IEnumerator coroutine)
    {
        if (coroutine != null) 
            Instance.StopCoroutine(coroutine);
    }

    private void OnDestroy() => 
        _instance = null;
}
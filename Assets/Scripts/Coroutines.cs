using System.Collections;
using UnityEngine;

public sealed class Coroutines:MonoBehaviour
{
    private static Coroutines _instance;

    private static Coroutines Instance { 
        get 
        {
            if (_instance == null)
            {
                var go = new GameObject("Coroutines controller");
                _instance = go.AddComponent<Coroutines>();
                DontDestroyOnLoad(go);
            }
            return _instance; 
        } 
    }

    public static Coroutine Start(IEnumerator enumerator)
    {
        return Instance.StartCoroutine(enumerator);
    }

    public static void Stop(Coroutine coroutine)
    {
        _instance.StopCoroutine(coroutine);
    }
}
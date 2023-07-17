using System.Collections;
using UnityEngine;
using VavilichevGD.Utils.Timing;

namespace Pool
{
    public class PoolObject : MonoBehaviour
    {
        private Coroutine _coroutine;

        public void Take(float delay)
        {
            StartCoroutine(ReturnDelay(delay));
        }

        IEnumerator ReturnDelay(float delayToBack)
        {
            yield return new WaitForSeconds(delayToBack);
            ReturnToPool();
        }

        public void ReturnToPool()
        {
            if (_coroutine!=null)
                StopCoroutine(_coroutine);

            gameObject.SetActive(false);
        }

        public void ReturnToPoolDelay(float delay)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            _coroutine = StartCoroutine(ReturnDelay(delay));
        }
    }
}
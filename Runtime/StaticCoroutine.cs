using System.Collections;
using UnityEngine;

namespace DoppleLittleHelper
{
    public class StaticCoroutine : MonoBehaviour
    {
        static StaticCoroutine _instance;

        static StaticCoroutine Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindAnyObjectByType<StaticCoroutine>();

                    if (_instance == null)
                        _instance = new GameObject("StaticCoroutine").AddComponent<StaticCoroutine>();
                }
                
                return _instance;
            }
        }

        void Awake()
        {
            if (_instance == null)
                _instance = this;
        }

        private IEnumerator Perform(IEnumerator coroutine)
        {
            yield return StartCoroutine(coroutine);
            Cleanup();
        }

        public static void DoCoroutine(IEnumerator coroutine)
        {
            Instance.StartCoroutine(Instance.Perform(coroutine));
        }

        private void Cleanup()
        {
            _instance = null;
            DestroyImmediate(gameObject);
        }

        void OnApplicationQuit()
        {
            _instance = null;
        }
    }
}
using System.Collections;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadingCurtain : MonoBehaviour, ICurtain
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        private void Awake() => 
            DontDestroyOnLoad(this);
        public void Show()
        {
            gameObject.SetActive(true);
            _canvasGroup.alpha = 1;
        }
        public void Hide() => 
            StartCoroutine(FideIn());

        private IEnumerator FideIn()
        {
            while (_canvasGroup.alpha > 0)
            {
                _canvasGroup.alpha -= 0.01f;
                yield return null;
            }

            gameObject.SetActive(false);
        }
    }
}

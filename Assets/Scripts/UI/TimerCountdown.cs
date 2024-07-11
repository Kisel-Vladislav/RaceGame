using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Scripts.UI
{
    public class TimerCountdown : MonoBehaviour
    {
        [SerializeField] private TMP_Text _timeText;
        [SerializeField] private float _delay;
        [SerializeField] private int _countdown;

        public event Action OnCompleted;

        private void Start()
        {
            _timeText.text = string.Empty;
            StartCoroutine(Countdown(_delay, _countdown));
        }

        private IEnumerator Countdown(float delay, int start)
        {
            var wait = new WaitForSeconds(delay);

            for (var i = start; i >= 0; i--)
            {
                DisplayCountdown(i);
                yield return wait;
            }
            OnCompleted?.Invoke();
            gameObject.SetActive(false);
        }
        private void DisplayCountdown<T>(T i) =>
            _timeText.text = i.ToString();
    }
}
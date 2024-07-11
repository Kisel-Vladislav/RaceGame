using Scripts.LevelLogic;
using System.Collections;
using UnityEngine;

namespace Scripts.UI
{
    public class Stopwatch : MonoBehaviour
    {
        private float _currentTimeInSeconds = 0;
        private Level _level;

        public TimeDisplay TimeDisplay;

        public float CurrentTimeInSeconds => _currentTimeInSeconds;

        public void Construct(Level level) =>
            _level = level;

        public void StartStopWatch() =>
            StartCoroutine(Run());

        private IEnumerator Run()
        {
            while (!_level.IsCompleted)
            {
                _currentTimeInSeconds += Time.deltaTime;
                RefreshDisplay(string.Format("{0:00.00}", _currentTimeInSeconds));
                yield return null;
            }
        }
        private void RefreshDisplay<T>(T i) =>
            TimeDisplay.Time = i.ToString();

    }
}
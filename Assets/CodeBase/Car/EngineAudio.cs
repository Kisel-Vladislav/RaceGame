using System.Collections;
using UnityEngine;

namespace CodeBase.Car
{
    public class EngineAudio : MonoBehaviour
    {
        [SerializeField] private AudioSource RunningEngine;
        [SerializeField] private float MaxPithRunning;
        [SerializeField] private float MinPithRunningAudio;
        [SerializeField] private float MinVolumeRunningAudio;
        [SerializeField] private float MaxVolumeRunningAudio;
        [SerializeField] private CarController CarController;
        [SerializeField] private float SpeedRatio;

        private void Start()
        {
            StartCoroutine(StartEngine());
            StartEngine();
        }
        private void Update()
        {
            SpeedRatio = CarController.GetSpeedRatio();
            RunningEngine.volume = Mathf.Lerp(MinVolumeRunningAudio, MaxVolumeRunningAudio, SpeedRatio);
            RunningEngine.pitch = Mathf.Lerp(RunningEngine.pitch, Mathf.Lerp(0.3f, MaxPithRunning, SpeedRatio) + MinPithRunningAudio, Time.deltaTime);
        }

        public IEnumerator StartEngine()
        {
            RunningEngine.volume = 0;
            RunningEngine.Play();

            while(RunningEngine.volume < MinVolumeRunningAudio)
            {
                RunningEngine.volume++;
                yield return null;
            }
        }

    }
}
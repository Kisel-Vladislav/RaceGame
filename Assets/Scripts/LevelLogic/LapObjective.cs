using System;
using UnityEngine;

namespace Scripts.LevelLogic
{
    public class LapObjective : MonoBehaviour
    {
        private const string CarTag = "Car";

        public event Action<LapObjective> OnPickup;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(CarTag))
                OnPickup?.Invoke(this);
        }
    }
}
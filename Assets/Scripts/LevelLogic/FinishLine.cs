using CodeBase.Car;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.LevelLogic
{
    public class FinishLine : MonoBehaviour
    {
        private List<LapObjective> _lapObjectives;

        public event Action OnFinished;

        public void Construct(List<LapObjective> obj)
        {
            _lapObjectives = obj;

            foreach (var item in _lapObjectives)
                item.OnPickup += LapObjectiveCompleted;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (IsLapObjectivesCompleted && other.GetComponent<PlayerCarController>())
            {
                OnFinished?.Invoke();
                gameObject.SetActive(false);
            }

        }

        private bool IsLapObjectivesCompleted => _lapObjectives.Count == 0;

        private void LapObjectiveCompleted(LapObjective objective) =>
            _lapObjectives.Remove(objective);

    }
}
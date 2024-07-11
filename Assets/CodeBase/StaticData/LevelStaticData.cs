using CodeBase.Infrastructure.Factory;
using Scripts;
using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "LevelStaticData", menuName = "Level/LevelStaticData")]
    public class LevelStaticData : ScriptableObject
    {
        public LevelId LevelId;
        public string SceneName;
        public WayPointContainer Waypoints;
        public LapObjectiveContainer LapObjectives;
        public GameObject[] RaceBarrier;
    }
}
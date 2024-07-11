using CodeBase.Car;
using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "CarData",menuName = "StaticData/Car")]
    public class CarStaticData : ScriptableObject
    {
        public CarTypeId CarTypeId;
        public GameObject Prefab;

        public float MaxSpeed;
        public float MotorPower;
        public float BrakePower;
    }
}

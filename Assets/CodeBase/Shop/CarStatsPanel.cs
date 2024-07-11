using CodeBase.StaticData;
using TMPro;
using UnityEngine;

namespace CodeBase.Shop
{
    public class CarStatsPanel : MonoBehaviour
    {
        [Header("Text")]
        [SerializeField] private TMP_Text MaxSpeed;
        [SerializeField] private TMP_Text MotorPower;
        [SerializeField] private TMP_Text BrakePower;

        public void Refresh(CarStaticData carStaticData)
        {
            MaxSpeed.text = carStaticData.MaxSpeed.ToString();
            MotorPower.text = carStaticData.MotorPower.ToString();
            BrakePower.text = carStaticData?.BrakePower.ToString();
        }
    }
}
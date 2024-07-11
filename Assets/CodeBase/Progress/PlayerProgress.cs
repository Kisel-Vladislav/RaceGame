using CodeBase.Car;
using CodeBase.Infrastructure.Factory;
using System;
using System.Collections.Generic;

namespace CodeBase.Progress
{
    [Serializable]
    public class PlayerProgress
    {
        public PlayerCars PlayerCars;
        public List<LevelData> LevelData;
        public int Money;

        public PlayerProgress()
        {
            PlayerCars = new PlayerCars();
            LevelData = new List<LevelData>()
            {
                new LevelData(LevelId.Level1),
                new LevelData(LevelId.Level2),
            };
            Money = 5000;
        }
    }

    [Serializable]
    public class LevelData
    {
        public LevelId Id;
        public float BestLopTimeInSeconds;

        public LevelData(LevelId id)
        {
            Id = id;
            BestLopTimeInSeconds = 0;
        }
    }

    [Serializable]
    public class PlayerCars
    {
        public CarTypeId CurrentCar;
        public List<CarTypeId> AllCars;

        public PlayerCars()
        {
            CurrentCar = CarTypeId.Dodge;
            AllCars = new List<CarTypeId>() { CurrentCar };
        }
    }
}

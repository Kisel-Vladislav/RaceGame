using CodeBase.Car;
using CodeBase.Infrastructure.Factory;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private static Dictionary<CarTypeId, CarStaticData> _cars;
        private Dictionary<LevelId, LevelStaticData> _levels;

        public CarStaticData ForCar(CarTypeId id) =>
            _cars.TryGetValue(id, out var carData) ? carData : null;
        public LevelStaticData ForLevel(LevelId id) => 
            _levels.TryGetValue(id, out var levelData) ? levelData : null;

        public void LoadCars()
        {
            _cars = Resources.LoadAll<CarStaticData>("StaticData/Cars")
                .ToDictionary(x => x.CarTypeId, x => x);
        }
        public void LoadLevel()
        {
            _levels = Resources.LoadAll<LevelStaticData>("StaticData/Levels")
             .ToDictionary(x => x.LevelId, x => x);
        }
    }
}

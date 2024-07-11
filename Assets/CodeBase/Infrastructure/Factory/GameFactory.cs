using CodeBase.Car;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Service;
using CodeBase.Infrastructure.Service.PausedService;
using CodeBase.StaticData;
using Scripts.LevelLogic;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticData;
        private readonly IPausedService _pausedService;

        public GameObject Player { get; private set; }
        public List<GameObject> AICars { get; private set; }

        public GameFactory(IAssetProvider assetProvider, IStaticDataService staticData, IPausedService pausedService)
        {
            _assetProvider = assetProvider;
            _staticData = staticData;
            _pausedService = pausedService;
        }

        public void CreatePlayerCar(CarTypeId id, Vector3 at, Quaternion rotation)
        {
            var carPlayer = CreateCar(id, at, rotation);

            var playerCarController = carPlayer.AddComponent<PlayerCarController>();
            playerCarController
                .Construct(new StandardInputService(), carPlayer.GetComponent<CarController>());

            Camera.main.GetComponent<CameraFollow>().Follow(carPlayer.transform);
        }
        public void CreateAICar(CarTypeId id, Vector3 at, Quaternion rotation, Transform[] waypoints)
        {
            var carAI = CreateCar(id, at, rotation);

            var agentCarController = carAI.AddComponent<AgentCarController>();
            agentCarController
                .Construct(waypoints, carAI.GetComponent<CarController>());

        }
        public List<LapObjective> CreateLapObjectives(LevelId id)
        {
            var levelData = _staticData.ForLevel(id);
            var lapObjectives = Object.Instantiate(levelData.LapObjectives);

            return lapObjectives.LapObjectives;
        }
        public GameObject CreateHUD() =>
            _assetProvider.Instance<GameObject>(AssetsPath.HUDPath);
        public void CleanupCode()
        {
            Player = null;
            AICars.Clear();
        }

        private GameObject CreateCar(CarTypeId id, Vector3 at, Quaternion rotation)
        {
            var carData = _staticData.ForCar(id);

            var car = Object.Instantiate(carData.Prefab, at, rotation);
            var carController = car.GetComponent<CarController>();
            carController.Construct(carData.MaxSpeed, carData.MotorPower, carData.BrakePower);
            _pausedService.Register(carController);

            return car;
        }
    }

    public enum LevelId
    {
        Level1,
        Level2,
    }
}


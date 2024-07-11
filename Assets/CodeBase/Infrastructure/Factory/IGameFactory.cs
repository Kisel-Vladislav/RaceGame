using Assets.CodeBase.Services;
using CodeBase.Car;
using Scripts.LevelLogic;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject Player { get; }
        List<GameObject> AICars { get; }

        GameObject CreateHUD();
        void CreateAICar(CarTypeId id, Vector3 at, Quaternion rotation, Transform[] waypoints);
        void CreatePlayerCar(CarTypeId id, Vector3 at, Quaternion rotation);
        List<LapObjective> CreateLapObjectives(LevelId id);

        void CleanupCode();
    }
}
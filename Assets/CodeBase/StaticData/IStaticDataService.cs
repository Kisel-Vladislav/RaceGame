using Assets.CodeBase.Services;
using CodeBase.Car;
using CodeBase.Infrastructure.Factory;

namespace CodeBase.StaticData
{
    public interface IStaticDataService : IService
    {
        CarStaticData ForCar(CarTypeId id);
        LevelStaticData ForLevel(LevelId id);

        void LoadCars();
        void LoadLevel();
    }
}
using CodeBase.Infrastructure.Factory;
using Data;
using UnityEngine;

namespace CodeBase.Car
{
    public class CarSpawner : MonoBehaviour
    {
        [SerializeField] private Transform[] _initialPoint;
        [SerializeField] private Vector3 _rotation;

        private IGameFactory _gameFactory;
        private CarTypeId _playerCar;
        private Transform[] _waypoints;

        public void Construct(IGameFactory gameFactory, CarTypeId playerCar, Transform[] waypoints)
        {
            _gameFactory = gameFactory;
            _playerCar = playerCar;
            _waypoints = waypoints;
        }
        public void Spawn()
        {
            _gameFactory.CreatePlayerCar(_playerCar, _initialPoint[0].transform.position, Quaternion.Euler(_rotation));

            var rand = new System.Random();
            for (var i = 1; i < _initialPoint.Length; i++)
                _gameFactory.CreateAICar(rand.GetEnumType<CarTypeId>(), _initialPoint[i].transform.position, Quaternion.Euler(_rotation), _waypoints);
        }
    }
}
using CodeBase.Infrastructure.Service;
using UnityEngine;

namespace CodeBase.Car
{
    public class PlayerCarController : MonoBehaviour
    {
        [SerializeField] private CarController CarController;
        private IInputService _inputService;
        public void Construct(IInputService inputService, CarController carController)
        {
            _inputService = inputService;
            CarController = carController;
        }
        private void Update() => 
            CarController.CheckInput(_inputService.Axis);
    }
}
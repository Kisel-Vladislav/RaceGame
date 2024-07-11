using UnityEngine;

namespace CodeBase.Car
{
    public class AgentCarController : MonoBehaviour
    {
        public CarController CarController;
        public Transform[] Waypoint;
        private int _currentWaypoint;
        private float _gasInput;
        private float _steeringInput;
        private GameObject _tracker;

        public void  Construct(Transform[] waypoint,CarController carController)
        {
            Waypoint = waypoint;
            CarController = carController;
        }
        private void Start() =>
            CreateTracker();
        private void Update()
        {
            ProgressTracker();
            var gasInput = 0f;
            var steeringInput = 0f;

            Vector3 dirToMovePosition = (_tracker.transform.position - transform.position).normalized;

            float dot = Vector3.Dot(transform.forward, dirToMovePosition);
            gasInput = dot > 0 ? 0.5f : -1;

            float angle = Vector3.SignedAngle(transform.forward, dirToMovePosition, Vector3.up);
            steeringInput = angle > 0 ? 0.5f : -1f;

            _gasInput = Mathf.Lerp(_gasInput, gasInput, Time.deltaTime);
            _steeringInput = Mathf.Lerp(_steeringInput, steeringInput, Time.deltaTime);

            CarController.CheckInput(new Vector2(_steeringInput, _gasInput));
        }

        private void CreateTracker()
        {
            _tracker = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            DestroyImmediate(_tracker.GetComponent<Collider>());
            DestroyImmediate(_tracker.GetComponent<MeshRenderer>());
            _tracker.transform.position = transform.position;
            _tracker.transform.rotation = transform.rotation;
        }
        private void ProgressTracker()
        {
            if (Vector3.Distance(Waypoint[_currentWaypoint].transform.position, _tracker.transform.position) < 15f)
                _currentWaypoint = (_currentWaypoint + 1) % Waypoint.Length;

            if (Vector3.Distance(transform.position, _tracker.transform.position) < 25f)
            {
                _tracker.transform.LookAt(Waypoint[_currentWaypoint].transform.position);
                _tracker.transform.Translate(0, 0, (CarController.SpeedClamped + 10f) * Time.deltaTime);
            }
        }

    }
}
using CodeBase.Infrastructure.Service.PausedService;
using UnityEngine;

namespace CodeBase.Car
{
    public class CarController : MonoBehaviour, IPaused
    {
        [SerializeField] private Rigidbody _playerRB;
        [SerializeField] private WheelColliders colliders;
        [SerializeField] private WheelMeshes wheelMeshes;
        [SerializeField] private WheelParticles wheelParticles;
        [SerializeField] private AnimationCurve steeringCurve;
        [SerializeField] private GameObject smokePrefab;

        private float _gasInput;
        private float _brakeInput;
        private float _steeringInput;
        private float _slipAngle;
        private float speed;
        private WheelHit[] _wheelHits = new WheelHit[2];
        private bool _isPaused;

        private float _maxSpeed;
        private float _motorPower;
        private float _brakePower;

        public float SpeedClamped;

        public void Construct(float maxSpeed, float motorPower, float brakePower)
        {
            _maxSpeed = maxSpeed;
            _motorPower = motorPower;
            _brakePower = brakePower;
        }
        private void Start()
        {
            _playerRB = gameObject.GetComponent<Rigidbody>();
            InstantiateSmoke();
        }
        private void Update()
        {
            if (_isPaused)
                return;

            speed = colliders.RRWheel.rpm / 60 * colliders.RRWheel.radius * 2f * Mathf.PI * 3.6f;
            SpeedClamped = Mathf.Lerp(SpeedClamped, speed, Time.deltaTime);

            ApplyMotor();
            ApplySteering();
            ApplyBrake();
            CheckParticles();
            ApplyWheelPositions();
        }

        public float GetSpeedRatio()
        {
            var gas = Mathf.Clamp(Mathf.Abs(_gasInput), 0.5f, 1f);
            return SpeedClamped * gas / _maxSpeed;
        }
        public void SetPaused(bool isPaused) => 
            _isPaused = isPaused;

        private void InstantiateSmoke()
        {
            wheelParticles.RRWheel = Instantiate(smokePrefab, colliders.RRWheel.transform.position - Vector3.up * colliders.FRWheel.radius, Quaternion.identity, colliders.RRWheel.transform)
                .GetComponent<ParticleSystem>();
            wheelParticles.RLWheel = Instantiate(smokePrefab, colliders.RLWheel.transform.position - Vector3.up * colliders.FRWheel.radius, Quaternion.identity, colliders.RLWheel.transform)
                .GetComponent<ParticleSystem>();
        }
        public void CheckInput(Vector2 input)
        {
            _steeringInput = input.x;
            _gasInput = input.y;
            _slipAngle = Vector3.Angle(transform.forward, _playerRB.velocity - transform.forward);

            float movingDirection = Vector3.Dot(transform.forward, _playerRB.velocity);
            if (movingDirection < -0.5f && _gasInput > 0)
                _brakeInput = Mathf.Abs(_gasInput);
            else if (movingDirection > 0.5f && _gasInput < 0)
                _brakeInput = Mathf.Abs(_gasInput);
            else
                _brakeInput = 0;

        }
        private void ApplyBrake()
        {
            colliders.FRWheel.brakeTorque = _brakeInput * _brakePower * 0.7f;
            colliders.FLWheel.brakeTorque = _brakeInput * _brakePower * 0.7f;

            colliders.RRWheel.brakeTorque = _brakeInput * _brakePower * 0.3f;
            colliders.RLWheel.brakeTorque = _brakeInput * _brakePower * 0.3f;
        }
        private void ApplyMotor()
        {
            if (speed >= _maxSpeed)
            {
                colliders.RRWheel.motorTorque = 0;
                colliders.RLWheel.motorTorque = 0;
            }
            else
            {
                colliders.RRWheel.motorTorque = _motorPower * _gasInput;
                colliders.RLWheel.motorTorque = _motorPower * _gasInput;
            }
        }
        private void ApplySteering()
        {
            float steeringAngle = _steeringInput * steeringCurve.Evaluate(speed);

            if (_slipAngle < 120f)
                steeringAngle += Vector3.SignedAngle(transform.forward, _playerRB.velocity + transform.forward, Vector3.up);

            steeringAngle = Mathf.Clamp(steeringAngle, -90f, 90f);
            colliders.FRWheel.steerAngle = steeringAngle;
            colliders.FLWheel.steerAngle = steeringAngle;
        }
        private void ApplyWheelPositions()
        {
            UpdateWheel(colliders.FRWheel, wheelMeshes.FRWheel);
            UpdateWheel(colliders.FLWheel, wheelMeshes.FLWheel);
            UpdateWheel(colliders.RRWheel, wheelMeshes.RRWheel);
            UpdateWheel(colliders.RLWheel, wheelMeshes.RLWheel);
        }
        private void CheckParticles()
        {
            colliders.RRWheel.GetGroundHit(out _wheelHits[0]);
            colliders.RLWheel.GetGroundHit(out _wheelHits[1]);

            float slipAllowance = 0.5f;
            if ((Mathf.Abs(_wheelHits[0].sidewaysSlip) + Mathf.Abs(_wheelHits[0].forwardSlip) > slipAllowance))
                wheelParticles.RRWheel.Play();
            else
                wheelParticles.RRWheel.Stop();

            if ((Mathf.Abs(_wheelHits[1].sidewaysSlip) + Mathf.Abs(_wheelHits[1].forwardSlip) > slipAllowance))
                wheelParticles.RLWheel.Play();
            else
                wheelParticles.RLWheel.Stop();


        }
        private void UpdateWheel(WheelCollider coll, MeshRenderer wheelMesh)
        {
            Quaternion quat;
            Vector3 position;
            coll.GetWorldPose(out position, out quat);
            wheelMesh.transform.position = position;
            wheelMesh.transform.rotation = quat;
        }
    }
    [System.Serializable]
    public class WheelColliders
    {
        public WheelCollider FRWheel;
        public WheelCollider FLWheel;
        public WheelCollider RRWheel;
        public WheelCollider RLWheel;
    }
    [System.Serializable]
    public class WheelMeshes
    {
        public MeshRenderer FRWheel;
        public MeshRenderer FLWheel;
        public MeshRenderer RRWheel;
        public MeshRenderer RLWheel;
    }
    [System.Serializable]
    public class WheelParticles
    {
        public ParticleSystem FRWheel;
        public ParticleSystem FLWheel;
        public ParticleSystem RRWheel;
        public ParticleSystem RLWheel;

    }
}
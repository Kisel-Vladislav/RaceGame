using UnityEngine;

namespace CodeBase.Car
{
    public class WheelControl : MonoBehaviour
    {
        [SerializeField] private Transform _wheelModel;

        private Vector3 _position;
        private Quaternion _rotation;

        [HideInInspector] public WheelCollider WheelCollider;
        public bool steerable;
        public bool motorized;

        private void Start() => 
            WheelCollider = GetComponent<WheelCollider>();
        private void Update()
        {
            WheelCollider.GetWorldPose(out _position, out _rotation);
            _wheelModel.transform.position = _position;
            _wheelModel.transform.rotation = _rotation;
        }
    }
}
using UnityEngine;
using UnityEngine.Events;

namespace DayOfSurvival.Scripts
{
    [RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(CapsuleCollider))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private JoyStick _joyStick;

        [Header("Features")]
        [Range(0, 12)]
        [SerializeField] private float _moveSpeed = 3f;
        [Range(0, 12)]
        [SerializeField] private float _rotationSpeed = 12f;
        [Range(0, 12)]
        [SerializeField] private float _rotationSpeedOnAim = 4f;

        private Rigidbody _rigidbody;
        private Transform _transform;

        private float _minMagnitude = 0;

        [HideInInspector] public UnityEvent<float> _moveEvent;
        [HideInInspector] public UnityEvent<bool> _aimEvent;

        private bool _isAim = false;
        private bool _isMove = false;

        private void OnEnable()
        {
            if (_joyStick != null)
            {
                _joyStick._dragEvent.AddListener(DragJoystickEvent);
                _joyStick._endDragEvent.AddListener(EndDragJoystickEvent);
            }
        }

        private void OnDisable()
        {
            if (_joyStick != null)
            {
                _joyStick._dragEvent.RemoveListener(DragJoystickEvent);
                _joyStick._endDragEvent.RemoveListener(EndDragJoystickEvent);
            }
        }

        private void Awake()
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (_isMove is true) MoveRealization();
            else _moveEvent?.Invoke(0);
        }

        private void DragJoystickEvent()
        {
            if (StickMagnitude > _minMagnitude)
                _isMove = true; else _isMove = false;
        }

        private void EndDragJoystickEvent()
        {
            _isMove = false;
        }

        private void MoveRealization()
        {
            if (_isAim is false) Move();

            Rotate();
            _moveEvent?.Invoke(StickMagnitude);
        }

        private void Move()
        {
            Vector3 position = Vector3.Lerp(_transform.position, _transform.position + _joyStick.Position, _moveSpeed * Time.fixedDeltaTime);
            _rigidbody.MovePosition(position);
        }

        private void Rotate()
        {
            float rotationSpeed;

            if (_isAim is false) rotationSpeed = _rotationSpeed;
            else rotationSpeed = _rotationSpeedOnAim;

            Quaternion direction = Quaternion.LookRotation(_joyStick.Position - _rigidbody.velocity);
            Quaternion rotation = Quaternion.Lerp(_rigidbody.rotation, direction, rotationSpeed * Time.fixedDeltaTime);

            _rigidbody.MoveRotation(rotation);
        }

        public void Aim()
        {
            if (_isAim is false)
                _aimEvent?.Invoke(_isAim = true);
            else _aimEvent?.Invoke(_isAim = false);
        }

        private float StickMagnitude { get => _joyStick.Position.magnitude; }
    }
}

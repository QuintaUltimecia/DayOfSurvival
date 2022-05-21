using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace DayOfSurvival.Scripts
{
    public class JoyStick : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        [SerializeField] private GameObject _analogStick;
        [SerializeField] private GameObject _borderStick;

        private Transform _stickTransform;
        private Transform _borderTransform;

        private float _maxRadius;

        private Vector3 _inputPosition;
        private Touch _touch;

        [HideInInspector] public UnityEvent _dragEvent = new UnityEvent();
        [HideInInspector] public UnityEvent _endDragEvent = new UnityEvent();

        private void Awake()
        {
            if (_analogStick == null) print("Analog stick needs to be installed.");
            if (_borderStick == null) print("Border stick needs to be installed.");

            _stickTransform = _analogStick.transform;
            _borderTransform = _borderStick.transform;

            _maxRadius = _borderStick.GetComponent<RectTransform>().sizeDelta.x / 2;
        }

        public void OnDrag(PointerEventData eventData)
        {
            GetTouch();

            Vector2 offset = _inputPosition - _borderTransform.position;
            _stickTransform.position = (Vector2)_borderTransform.position + Vector2.ClampMagnitude(offset, _maxRadius);

            _dragEvent?.Invoke();
        }

        public void OnEndDrag(PointerEventData eventData) 
        {
            _stickTransform.localPosition = Vector3.zero;

            _endDragEvent?.Invoke();
        }

        private void GetTouch()
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                for (int i = 0; i < Input.touchCount; ++i)
                    if (Input.GetTouch(i).phase == TouchPhase.Moved)
                        _touch = Input.GetTouch(i);

                _inputPosition = _touch.position;
            }
            else
                _inputPosition = Input.mousePosition;
        }

        private Vector3 PositionConvertor(Vector3 newPosition = new Vector3())
        {
            newPosition = _stickTransform.position - _borderTransform.position;
            newPosition = new Vector3(x: newPosition.x, y: 0, z: newPosition.y);

            return newPosition * Time.deltaTime;
        }

        private Vector3 RotationConvertor(Vector3 newRotation = new Vector3())
        {
            newRotation = _stickTransform.position - _borderTransform.position;
            newRotation = new Vector3(x: 0, y: newRotation.y, z: 0);

            return newRotation * Time.deltaTime;
        }

        public Vector3 Position { get => PositionConvertor(); }
        public Vector3 Rotation { get => RotationConvertor(); }
    }
}

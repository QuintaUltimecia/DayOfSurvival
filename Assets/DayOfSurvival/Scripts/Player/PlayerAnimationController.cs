using UnityEngine;

namespace DayOfSurvival.Scripts
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private Character _character;

        private Animator _animator;

        private readonly int _moveName = Animator.StringToHash("Move");
        private readonly int _aimName = Animator.StringToHash("isAim");
        private readonly int _deathName = Animator.StringToHash("isDeath");

        private void Awake() => _animator = GetComponent<Animator>();

        private void OnEnable() 
        { 
            if (_playerController == null) print("PlayerController is null.");
            else
            {
                _playerController._moveEvent.AddListener(ApplyMoveAnimation);
                _playerController._aimEvent.AddListener(ApplyAimAnimation);
            }

            if (_character == null) print("Character is null");
            else
            {
                _character._deathEvent.AddListener(ApplyDeathAnimation);
            }
        }

        private void OnDisable()
        {
            if (_playerController == null) print("PlayerController is null.");
            else
            {
                _playerController._moveEvent.RemoveListener(ApplyMoveAnimation);
                _playerController._aimEvent.RemoveListener(ApplyAimAnimation);
            }

            if (_character == null) print("Character is null");
            else
            {
                _character._deathEvent.RemoveListener(ApplyDeathAnimation);
            }
        }

        public void ApplyMoveAnimation(float value) => _animator.SetFloat(_moveName, value);
        public void ApplyAimAnimation(bool isActive) => _animator.SetBool(_aimName, isActive);
        public void ApplyDeathAnimation(bool isActive) => _animator.SetBool(_deathName, isActive);
    }
}

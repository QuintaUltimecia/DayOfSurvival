using UnityEngine;

namespace DayOfSurvival.Scripts
{
    [RequireComponent(typeof(LineRenderer))]
    public class LaserForWeapon : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;
        [SerializeField] private PlayerController _playerController;

        private LineRenderer _laser;

        private Transform _bulletSpawn;
        private float _shootDistance;

        private void OnEnable()
        {
            _playerController._aimEvent.AddListener(ActiveLaser);
        }

        private void OnDisable()
        {
            _playerController._aimEvent.RemoveListener(ActiveLaser);
        }

        private void Awake()
        {
            _laser = GetComponent<LineRenderer>();
            _bulletSpawn = _weapon.BulletSpawn;
            _shootDistance = _weapon.ShootDistance;
        }

        private void Start()
        {
            SetLaser();
            ActiveLaser(false);
        }

        private void SetLaser()
        {
            _laser.positionCount = 2;

            _laser.SetPosition(0, _bulletSpawn.position - transform.position);
            _laser.SetPosition(1, new Vector3(
                _laser.GetPosition(0).x * (_shootDistance * 2),
                _laser.GetPosition(0).y,
                _laser.GetPosition(0).z));
        }

        public void ActiveLaser(bool isActive) => _laser.enabled = isActive;
    }
}

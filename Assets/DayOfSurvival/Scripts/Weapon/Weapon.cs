using UnityEngine;
using UnityEngine.Events;

namespace DayOfSurvival.Scripts
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private Transform _bulletSpawn;

        private int _shootDistance = 50;
        private int _damage = 10;

        [HideInInspector] public UnityEvent _shootEvent = new UnityEvent();

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;

            Gizmos.DrawRay(_bulletSpawn.position, _bulletSpawn.right * _shootDistance);
        }

        public void Shoot()
        {
            RaycastHit hit;

            if (Physics.Raycast(_bulletSpawn.position, _bulletSpawn.right, out hit, _shootDistance))
            {
                if (hit.transform.TryGetComponent(out IGetDamage target))
                {
                    target.GetDamage(_damage);
                }
            }

            _shootEvent?.Invoke();
        }

        public Transform BulletSpawn { get => _bulletSpawn; }
        public int ShootDistance { get => _shootDistance; }
    }
}

using UnityEngine;
using UnityEngine.Events;

namespace DayOfSurvival.Scripts
{
    public abstract class Character : MonoBehaviour, IGetDamage
    {
        [SerializeField] private int _health;

        [HideInInspector] public UnityEvent<bool> _deathEvent;

        public void GetDamage(int damage)
        {
            Health -= damage;

            if (Health == 0)
                _deathEvent?.Invoke(true);
        }

        public int Health { get => _health; private set => _health = Mathf.Max(0, value); }
    }
}

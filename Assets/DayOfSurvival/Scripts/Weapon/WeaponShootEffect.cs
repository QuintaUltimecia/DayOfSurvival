using UnityEngine;

namespace DayOfSurvival.Scripts
{
    [RequireComponent(typeof(ParticleSystem))]
    public class WeaponShootEffect : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;

        private ParticleSystem _particleSystem;

        private void Awake() => _particleSystem = GetComponent<ParticleSystem>();

        private void OnEnable() => _weapon._shootEvent.AddListener(PlayParticleSystem);

        private void OnDisable() => _weapon._shootEvent.RemoveListener(PlayParticleSystem);

        private void PlayParticleSystem() => _particleSystem.Play();
    }
}

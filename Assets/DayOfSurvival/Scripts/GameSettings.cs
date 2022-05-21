using UnityEngine;

namespace DayOfSurvival.Scripts
{
    public class GameSettings : MonoBehaviour
    {
        [SerializeField] private int _targetFrameRate = 60;

        private void Start()
        {
            Application.targetFrameRate = _targetFrameRate;
        }
    }
}

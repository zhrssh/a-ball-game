using UnityEngine;

namespace DevZhrssh.Managers.Components
{
    public class TimeComponent : MonoBehaviour
    {
        // Time
        [Header("Time")]
        public float startTime;
        public float maxTime;
        public bool isCountdown;

        // Current Time
        private float _currentTime;
        public float currentTime { get { return _currentTime; } }

        // Public functions
        public void AddTime(float time)
        {
            if (isCountdown)
                _currentTime -= time;
            else
                _currentTime += time;
        }

        public void SetTime(float time)
        {
            _currentTime = time;
        }

        public void ResetTime()
        {
            if (isCountdown)
                _currentTime = startTime;
            else
                _currentTime = 0;
        }
    }
}


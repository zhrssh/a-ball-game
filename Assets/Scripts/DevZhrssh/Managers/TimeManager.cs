using UnityEngine;
using DevZhrssh.Managers.Components;

namespace DevZhrssh.Managers
{
    [RequireComponent(typeof(TimeComponent))]
    public class TimeManager : MonoBehaviour
    {
        private TimeComponent timeComponent;

        // When timer reached zero we call subscribed functions
        public delegate void OnTimeReachedZero();
        public event OnTimeReachedZero OnTimeReachedZeroCallback;

        // When timer reached limit we call subscribed functions
        public delegate void OnTimeReachedLimit();
        public event OnTimeReachedLimit OnTimeReachedLimitCallback;

        private void Start()
        {
            timeComponent = GetComponent<TimeComponent>();
            timeComponent.ResetTime();
        }

        private void Update()
        {
            timeComponent.AddTime(Time.deltaTime);
            if (timeComponent.currentTime <= 0f) // time reached zero
            {
                if (OnTimeReachedZeroCallback != null)
                    OnTimeReachedZeroCallback.Invoke();
            }

            if (timeComponent.currentTime >= timeComponent.maxTime) // time reached limit
            {
                if (OnTimeReachedLimitCallback != null)
                    OnTimeReachedLimitCallback.Invoke();
            }
        }
    }
}

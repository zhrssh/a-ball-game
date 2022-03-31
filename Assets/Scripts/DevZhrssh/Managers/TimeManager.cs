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

        public bool runTime = true;

        private void Start()
        {
            timeComponent = GetComponent<TimeComponent>();
            timeComponent.ResetTime();
        }

        private void Update()
        {
            if (!runTime) return; // if time has not yet started, return

            timeComponent.AddTime(Time.unscaledDeltaTime); // TODO: still needs fixing because time starts while game is loading
            if (timeComponent.isCountdown) // if timer is a countdown
            {
                if (timeComponent.currentTime <= 0f) // time reached zero
                {
                    if (OnTimeReachedZeroCallback != null)
                        OnTimeReachedZeroCallback.Invoke();

                    runTime = false; // stop time
                }
            }
            else // if timer is a stopwatch
            {
                if (timeComponent.currentTime >= timeComponent.maxTime) // time reached limit
                {
                    if (OnTimeReachedLimitCallback != null)
                        OnTimeReachedLimitCallback.Invoke();

                    runTime = false; // stop time
                }
            }
        }
    }
}

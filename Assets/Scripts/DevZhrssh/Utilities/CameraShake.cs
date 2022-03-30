using System.Collections;
using UnityEngine;
using DevZhrssh.Managers.Components;

namespace DevZhrssh.Utilities
{
    // The camera that will hold this script needs to have a parent holder in order for this to work as intended.
    public class CameraShake : MonoBehaviour
    {
        [Header("Camera Shake")]
        [SerializeField] private float shakeDuration;
        [SerializeField] private float shakeMagnitude;

        [Header("Player Death Component")]
        [SerializeField] private PlayerDeathComponent playerDeathComponent;

        private void Start()
        {
            if (playerDeathComponent != null)
                playerDeathComponent.onPlayerDeathCallback += StartShake;
        }

        private void StartShake()
        {
            // Starts shake;
            StartCoroutine(Shake(shakeDuration, shakeMagnitude));
        }

        public IEnumerator Shake(float duration, float magnitude)
        {
            Vector3 originalPos = transform.localPosition;
            float elapsedTime = 0.0f;

            while (elapsedTime < duration)
            {
                float x = Random.Range(-1f, 1f) * magnitude;
                float y = Random.Range(-1f, 1f) * magnitude;

                transform.localPosition = new Vector3(x, y, originalPos.z);
                elapsedTime += Time.deltaTime;

                yield return null;
            }

            transform.localPosition = originalPos;
        }
    }
}

using System.Collections;
using UnityEngine;

namespace DevZhrssh.Utilities
{
    // The camera that will hold this script needs to have a parent holder in order for this to work as intended.
    public class CameraShake : MonoBehaviour
    {
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

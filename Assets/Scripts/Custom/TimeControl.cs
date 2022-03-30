using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControl : MonoBehaviour
{
    public void SlowTime(float amount)
    {
        Time.timeScale = amount;
        Time.fixedDeltaTime = Time.timeScale * 0.02f; // avoids game looking like it's lagging
    }

    public void EndSlowTime()
    {
        Time.timeScale = 1f;
    }
}

using UnityEngine;
using DevZhrssh.Managers.Components;
using DevZhrssh.Managers;

public class TimeStopCallback : MonoBehaviour
{
    private TimeManager timeManager;
    private PlayerDeathComponent playerDeathComponent;

    private void Start()
    {
        var obj1 = GameObject.FindObjectOfType<TimeManager>();
        if (obj1 != null)
            timeManager = obj1 as TimeManager;

        var obj2 = GameObject.FindObjectOfType<PlayerDeathComponent>();
        if (obj2 != null)
            playerDeathComponent = obj2 as PlayerDeathComponent;

        if (playerDeathComponent != null)
            playerDeathComponent.onPlayerDeathCallback += StopTime;
    }

    public void StopTime()
    {
        timeManager.runTime = false;
    }

    public void ResumeTime()
    {
        timeManager.runTime = true;
    }
}

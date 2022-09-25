using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShopNavigation : MonoBehaviour
{
    public delegate void OnBallSelected(int id);
    public event OnBallSelected OnBallSelectedCallback;

    private void Start()
    {
        if (OnBallSelectedCallback != null)
            OnBallSelectedCallback.Invoke(0); // Select the first ball in the list
    }

    public void SelectBall(int id)
    {
        if (OnBallSelectedCallback != null)
            OnBallSelectedCallback.Invoke(id);
    }
}

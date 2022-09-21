using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UICoinCount : MonoBehaviour
{
    [SerializeField] private CoinCount coinCountScript;
    [SerializeField] private TextMeshProUGUI[] coinDisplay;

    // Update is called once per frame
    private void Start()
    {
        UpdateCount();
    }

    public void UpdateCount()
    {
        for (int i = 0; i < coinDisplay.Length; i++)
        {
            coinDisplay[i].text = coinCountScript.GetCoinCount().ToString();
        }
    }
}

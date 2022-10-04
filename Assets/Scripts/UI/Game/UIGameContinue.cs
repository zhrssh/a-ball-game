using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using DevZhrssh.Managers;

public class UIGameContinue : MonoBehaviour
{
    // Ads manager
    private AdsManager adsManager;

    private Button continueButtonComp;
    private void Awake()
    {
        continueButtonComp = GetComponent<Button>();
        adsManager = GameObject.FindObjectOfType<AdsManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Subscribe to callback
        if (adsManager != null)
        {
            adsManager.onAdLoadStartCallback += DisableButton;
            adsManager.onAdLoadEndCallback += EnableButton;
        }

        // Handles button interaction
        if (continueButtonComp != null)
        {
            // Disable the button first
            continueButtonComp.interactable = false;
            if (adsManager != null)
            {
                // Enable the button after few seconds if there are no ads loading
                if (adsManager.isAdLoading == false)
                    StartCoroutine(EnableInteractable());
            }
        }
    }

    private void EnableButton()
    {
        if (continueButtonComp != null)
            continueButtonComp.interactable = true;
    }

    private void DisableButton()
    {
        if (continueButtonComp != null)
            continueButtonComp.interactable = false;
    }

    // Responsible for enabling interactable button
    private IEnumerator EnableInteractable()
    {
        yield return new WaitForSecondsRealtime(1f);
        continueButtonComp.interactable = true;
    }
}

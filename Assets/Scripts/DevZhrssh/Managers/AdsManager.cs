using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

namespace DevZhrssh.Managers
{
    public class AdsManager : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener, IUnityAdsInitializationListener
    {
        #region SINGLETON

        private static AdsManager _instance;
        public static AdsManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = GameObject.FindObjectOfType<AdsManager>();

                return _instance;
            }
        }

        #endregion

        private string gameId = "4717852";

        [SerializeField] private BannerPosition bannerPosition;
        private bool isTestMode;

        public delegate void OnAdCompleted();
        public event OnAdCompleted onAdCompletedCallback;

        private void Start()
        {
            if (Debug.isDebugBuild)
            {
                Advertisement.debugMode = true;
                isTestMode = true;
            } 
            else
            {
                Advertisement.debugMode = false;
                isTestMode = false;
            }

            Advertisement.Initialize(gameId, isTestMode, this); // Android ID
        }

        public void PlayAd()
        {
            // Load the Ad unit (Insterstitial Ads) 
            Advertisement.Load("Interstitial_Android", AdsManager.Instance as IUnityAdsLoadListener);
        }

        public void PlayRewardedAd()
        {
            // Load the Ad unity (Rewarded Ads)
            Advertisement.Load("Rewarded_Android", AdsManager.Instance as IUnityAdsLoadListener);
        }

        public void ShowBanner()
        {
            // Load the Ad unit (Banner Ads)
            Advertisement.Load("Banner_Android", AdsManager.Instance as IUnityAdsLoadListener);
        }

        public void HideBanner()
        {
            // Hides the banner
            Advertisement.Banner.Hide();
        }

        private IEnumerator RepeatShowBanner()
        {
            yield return new WaitForSeconds(1f);
            ShowBanner();
        }

        public void OnUnityAdsAdLoaded(string placementId)
        {
            // If the ad is a banner
            if (placementId.Equals("Banner_Android"))
            {
                Advertisement.Banner.SetPosition(bannerPosition);
                Advertisement.Banner.Show(placementId);
                return;
            }

            // When Ad is Successfully loaded
            Advertisement.Show(placementId, AdsManager.Instance as IUnityAdsShowListener);
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            // If the ad is a banner and it failed to load
            if (placementId.Equals("Banner_Android"))
            {
                StartCoroutine(RepeatShowBanner());
            }
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            // TODO: Show error message
        }

        public void OnUnityAdsShowStart(string placementId)
        {
            // No Implementation.
        }

        public void OnUnityAdsShowClick(string placementId)
        {
            // No Implementation.
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            // If it's a rewarded ad, we reward the player
            if (placementId.Equals("Rewarded_Android") && showCompletionState == UnityAdsShowCompletionState.COMPLETED)
            {
                // We invoke all functions subscribed to the delegate
                // Example: Rewarding the player
                if (onAdCompletedCallback != null)
                    onAdCompletedCallback.Invoke();
            }
        }

        public void OnInitializationComplete()
        {
            // No Implementation.
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            // No Implementation.
        }
    }
}

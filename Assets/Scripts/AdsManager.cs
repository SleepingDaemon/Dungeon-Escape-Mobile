using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private Button     _showAdButton;
    [SerializeField] private string     _androidGameId;
    [SerializeField] private string     _androidAdUnitId = "Rewarded_Android";
    [SerializeField] private bool       _testMode = true;

    private void Awake()
    {
        InitializeAds();
    }

    public void ShowRewardedAd()
    {
        Debug.Log("Showing rewarded ad");
        if(Advertisement.isInitialized)
            Advertisement.Show(_androidAdUnitId, this);
    }

    public void InitializeAds()
    {
        Advertisement.Initialize(_androidGameId, _testMode, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads Initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error} - {message}");
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        if (_androidAdUnitId.Equals(_androidAdUnitId))
        {
            _showAdButton.onClick.AddListener(ShowRewardedAd);
        }
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {_androidAdUnitId} - {error} - {message}");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {_androidAdUnitId}: {error} - {message}");
    }

    public void OnUnityAdsShowStart(string placementId) { }

    public void OnUnityAdsShowClick(string placementId) { }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if(_androidAdUnitId.Equals(_androidAdUnitId) && showCompletionState.Equals(UnityAdsCompletionState.COMPLETED))
        {
            Debug.Log("You Finished the ad. You receive 100G!");
            GameManager.Instance.AddGems(100);
            UIManager.Instance.UpdateShopGemCount(GameManager.Instance.GetGemsAmount());
        }
        else if(_androidAdUnitId.Equals(_androidAdUnitId) && showCompletionState.Equals(UnityAdsCompletionState.SKIPPED))
        {
            Debug.Log("You skipped the ad");
        }
        else if(_androidAdUnitId.Equals(_androidAdUnitId) && showCompletionState.Equals(UnityAdsCompletionState.UNKNOWN))
        {
            Debug.Log("Couldn't show ad for Unknown reasons");
        }
    }
}

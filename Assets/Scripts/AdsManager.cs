using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private Button     _showAdButton;
    [SerializeField] private string     _androidGameId;
    [SerializeField] private string     _iOSGameId;
    [SerializeField] private string     _androidAdUnitId;
    [SerializeField] private string     _iOsAdUnitId;
    [SerializeField] private bool       _testMode = true;

    private string _gameId;
    private string _adUnitId;

    private void Awake()
    {
        InitializeAds();
    }

    private void Start()
    {
        // Get the Ad Unit ID for the current platform:
        _adUnitId = null; // This will remain null for unsupported platforms
#if UNITY_IOS
        _adUnitId = _iOsAdUnitId;
        _showAdButton.enabled = true;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
        _showAdButton.enabled = true;
#endif
    }

    public void LoadRewardedAd()
    {
        Debug.Log("Ad Loaded: " + _adUnitId);

        if (Advertisement.isInitialized)
        {
            Advertisement.Load(_adUnitId, this);

        }

    }

    public void ShowRewardedAd()
    {
        Debug.Log("Showing rewarded ad");
        //_showAdButton.interactable = true;
        Advertisement.Show(_adUnitId);
    }

    public void InitializeAds()
    {
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOSGameId
            : _androidGameId;
        Advertisement.Initialize(_gameId, _testMode, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads Initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error} - {message}");
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);

        if (adUnitId.Equals(_adUnitId))
        {
            _showAdButton.onClick.AddListener(ShowRewardedAd);
            _showAdButton.interactable = true;
        }
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {adUnitId} - {error} - {message}");
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error} - {message}");
    }

    public void OnUnityAdsShowStart(string placementId) { }

    public void OnUnityAdsShowClick(string placementId) { }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if(adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsCompletionState.COMPLETED))
        {
            Debug.Log("You Finished the ad. You receive 100G!");
            GameManager.Instance.AddGems(100);
            UIManager.Instance.UpdateShopGemCount(GameManager.Instance.GetGemsAmount());
        }
        else if(adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsCompletionState.SKIPPED))
        {
            Debug.Log("You skipped the ad");
        }
        else if(adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsCompletionState.UNKNOWN))
        {
            Debug.Log("Couldn't show ad for Unknown reasons");
        }
    }
}

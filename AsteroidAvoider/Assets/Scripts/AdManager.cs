using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
  [SerializeField]
  bool testMode = true;

  public static AdManager Instance;

  GameOverHandler _gameOverHandler;
  const string RewardedVideo = "rewardedVideo";

#if UNITY_IOS
  string _gameId = "5116287";
#elif UNITY_ANDROID
  string _gameId = "5116286";
#endif

  void Awake()
  {
    if (Instance != null && Instance != this)
    {
      Destroy(gameObject);
    }
    else
    {
      Instance = this;
      DontDestroyOnLoad(gameObject);
      Advertisement.Initialize(_gameId, testMode, this);
    }
  }

  public void OnInitializationComplete()
  {
    Debug.Log("Unity Ads initialization complete.");
  }

  public void OnInitializationFailed(UnityAdsInitializationError error, string message)
  {
    Debug.LogError($"Unity Ads initialization failed. {error} - {message}");
  }

  public void OnUnityAdsAdLoaded(string placementId)
  {
    Debug.Log($"Ad loaded: {placementId}");
  }

  public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
  {
    Debug.LogError($"Error loading Ad Unit {placementId}. {error} - {message}");
  }

  public void OnUnityAdsShowClick(string placementId)
  {
  }

  public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
  {
    switch (showCompletionState)
    {
      case UnityAdsShowCompletionState.COMPLETED:
        _gameOverHandler.ContinueGame();
        break;
      case UnityAdsShowCompletionState.SKIPPED:
        //* Ad was skipped
        break;
      case UnityAdsShowCompletionState.UNKNOWN:
        Debug.LogWarning("Ad failed");
        break;
    }
  }

  public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
  {
    Debug.LogError($"Error showing Ad Unit {placementId}. {error} - {message}");
  }

  public void OnUnityAdsShowStart(string placementId)
  {
  }
  public void ShowAd(GameOverHandler gameOverHandler)
  {
    this._gameOverHandler = gameOverHandler;
    Advertisement.Show(RewardedVideo, this);
  }

}

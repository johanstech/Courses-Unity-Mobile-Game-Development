using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
  [SerializeField]
  TMP_Text highscoreText;
  [SerializeField]
  TMP_Text playButtonText;
  [SerializeField]
  Button playButton;
  [SerializeField]
  IOSNotificationHandler iOSNotificationHandler;
  [SerializeField]
  AndroidNotificationHandler androidNotificationHandler;
  [SerializeField]
  int maxEnergy;
  [SerializeField]
  int energyRechageDuration;

  const string EnergyKey = "Energy";
  const string EnergyReadyKey = "EnergyReady";

  int energy;

  void Start()
  {
    OnApplicationFocus(true);
  }

  void OnApplicationFocus(bool hasFocus)
  {
    if (!hasFocus) return;
    CancelInvoke();
    int highscore = PlayerPrefs.GetInt(ScoreSystem.HighscoreKey, 0);
    highscoreText.text = $"Highscore: {highscore}";
    energy = PlayerPrefs.GetInt(EnergyKey, maxEnergy);
    if (energy == 0)
    {
      string energyReadyString = PlayerPrefs.GetString(EnergyReadyKey, string.Empty);
      if (energyReadyString == string.Empty) return;
      DateTime energyReady = DateTime.Parse(energyReadyString);
      if (DateTime.Now > energyReady)
      {
        energy = maxEnergy;
        PlayerPrefs.SetInt(EnergyKey, energy);
      }
      else
      {
        playButton.interactable = false;
        Invoke(nameof(EnergyRecharged), (energyReady - DateTime.Now).Seconds);
      }
    }

    playButtonText.text = $"Play ({energy})";
  }

  void EnergyRecharged()
  {
    playButton.interactable = true;
    energy = maxEnergy;
    PlayerPrefs.SetInt(EnergyKey, energy);
    playButtonText.text = $"Play ({energy})";
  }

  public void Play()
  {
    if (energy < 1) return;
    energy--;
    PlayerPrefs.SetInt(EnergyKey, energy);
    if (energy == 0)
    {
      DateTime energyReady = DateTime.Now.AddMinutes(energyRechageDuration);
      PlayerPrefs.SetString(EnergyReadyKey, energyReady.ToString());
#if UNITY_IOS
      iOSNotificationHandler.ScheduleNotification(energyRechageDuration);
#elif UNITY_ANDROID
      androidNotificationHandler.ScheduleNotification(energyReady);
#endif
    }
    SceneManager.LoadScene("Game");
  }
}

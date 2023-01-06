using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  [SerializeField]
  TMP_Text highscoreText;
  [SerializeField]
  TMP_Text energyText;
  [SerializeField]
  int maxEnergy;
  [SerializeField]
  int energyRechageDuration;

  const string EnergyKey = "Energy";
  const string EnergyReadyKey = "EnergyReady";

  int energy;

  void Start()
  {
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
    }

    energyText.text = $"Play ({energy})";
  }

  public void Play()
  {
    if (energy < 1) return;
    energy--;
    PlayerPrefs.SetInt(EnergyKey, energy);
    if (energy == 0)
    {
      DateTime oneMinute = DateTime.Now.AddMinutes(energyRechageDuration);
      PlayerPrefs.SetString(EnergyReadyKey, oneMinute.ToString());
    }
    SceneManager.LoadScene("Game");
  }
}

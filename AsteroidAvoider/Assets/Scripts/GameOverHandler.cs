using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
  [SerializeField]
  TMP_Text gameOverText;
  [SerializeField]
  GameObject gameOverDisplay;
  [SerializeField]
  ScoreSystem scoreSystem;
  [SerializeField]
  AsteroidSpawner asteroidSpawner;

  public void EndGame()
  {
    asteroidSpawner.enabled = false;
    int finalScore = scoreSystem.StopTimer();
    gameOverText.text = $"Score: {finalScore}";
    gameOverDisplay.gameObject.SetActive(true);
  }

  public void PlayAgain()
  {
    SceneManager.LoadScene("Game");
  }

  public void BackToMainMenu()
  {
    SceneManager.LoadScene("MainMenu");
  }

  public void Continue()
  {
    //* Ads here
  }
}

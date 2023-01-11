using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
  [SerializeField]
  GameObject gameOverDisplay;
  [SerializeField]
  AsteroidSpawner asteroidSpawner;

  public void EndGame()
  {
    asteroidSpawner.enabled = false;
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

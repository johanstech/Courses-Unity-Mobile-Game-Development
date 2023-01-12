using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
  [SerializeField]
  GameObject player;
  [SerializeField]
  Button continueButton;

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

  public void ContinueButton()
  {
    AdManager.Instance.ShowAd(this);
    continueButton.interactable = false;
  }

  public void BackToMainMenu()
  {
    SceneManager.LoadScene("MainMenu");
  }

  public void ContinueGame()
  {
    scoreSystem.StartTimer();
    player.transform.position = Vector3.zero;
    player.SetActive(true);
    player.GetComponent<Rigidbody>().velocity = Vector3.zero;
    asteroidSpawner.enabled = true;
    gameOverDisplay.gameObject.SetActive(false);
  }
}

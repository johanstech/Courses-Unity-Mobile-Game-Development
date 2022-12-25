using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  [SerializeField]
  TMP_Text highscoreText;

  void Start()
  {
    int highscore = PlayerPrefs.GetInt(ScoreSystem.HighscoreKey, 0);
    highscoreText.text = $"Highscore: {highscore}";
  }

  public void Play()
  {
    SceneManager.LoadScene("Game");
  }
}

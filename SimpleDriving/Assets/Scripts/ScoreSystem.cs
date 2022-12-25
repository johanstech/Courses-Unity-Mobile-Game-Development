using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
  [SerializeField]
  TMP_Text scoreText;
  [SerializeField]
  float scoreMultiplier = 1;

  public const string HighscoreKey = "Highscore";

  float _score;

  void Update()
  {
    _score += Time.deltaTime * scoreMultiplier;
    scoreText.text = Mathf.FloorToInt(_score).ToString();
  }

  void OnDestroy()
  {
    int currentHighscore = PlayerPrefs.GetInt(HighscoreKey, 0);
    if (_score > currentHighscore)
    {
      PlayerPrefs.SetInt(HighscoreKey, Mathf.FloorToInt(_score));
      PlayerPrefs.Save();
    }
  }
}

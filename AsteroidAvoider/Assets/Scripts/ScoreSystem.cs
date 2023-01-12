using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
  [SerializeField]
  TMP_Text scoreText;
  [SerializeField]
  float scoreMultiplier = 5;

  float _score;
  bool _updateScore = true;

  void Update()
  {
    if (!_updateScore) return;

    _score += Time.deltaTime * scoreMultiplier;
    scoreText.text = Mathf.FloorToInt(_score).ToString();
  }

  public int StopTimer()
  {
    _updateScore = false;
    scoreText.text = string.Empty;
    return Mathf.FloorToInt(_score);
  }

  public void StartTimer()
  {
    _updateScore = true;
  }
}

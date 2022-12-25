using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
  [SerializeField]
  TMP_Text scoreText;
  [SerializeField]
  float scoreMultiplier = 1;

  float _score;

  void Update()
  {
    _score += Time.deltaTime * scoreMultiplier;
    scoreText.text = Mathf.FloorToInt(_score).ToString();
  }
}

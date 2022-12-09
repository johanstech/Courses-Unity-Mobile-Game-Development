using UnityEngine;

public class Car : MonoBehaviour
{
  [SerializeField]
  float moveSpeed = 10f;
  [SerializeField]
  float accelerationSpeed = 0.5f;
  [SerializeField]
  float turnSpeed = 200f;

  int _steerValue;

  void Update()
  {
    Move();
  }

  void Move()
  {
    moveSpeed += accelerationSpeed * Time.deltaTime;
    transform.Rotate(0f, _steerValue * turnSpeed * Time.deltaTime, 0f);
    transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
  }

  public void Steer(int value)
  {
    _steerValue = value;
  }
}

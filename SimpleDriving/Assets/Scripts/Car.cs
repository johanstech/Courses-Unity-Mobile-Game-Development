using UnityEngine;

public class Car : MonoBehaviour
{
  [SerializeField]
  float moveSpeed = 10f;
  [SerializeField]
  float accelerationSpeed = 0.5f;

  void Update()
  {
    moveSpeed += accelerationSpeed * Time.deltaTime;

    transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
  }
}

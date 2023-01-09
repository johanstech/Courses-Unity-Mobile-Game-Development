using UnityEngine;

public class Asteroid : MonoBehaviour
{
  void OnTriggerEnter(Collider other)
  {
    PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
    if (playerHealth == null) return;

    playerHealth.Crash();
  }
}

using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
  [SerializeField]
  GameObject[] asteroidPrefabs;
  [SerializeField]
  float secondsBetweenAsteroids = 0.5f;
  [SerializeField]
  Vector2 forceRange;

  Camera _mainCamera;
  float _timer;

  void Start()
  {
    _mainCamera = Camera.main;
  }

  void Update()
  {
    _timer -= Time.deltaTime;

    if (_timer <= 0)
    {
      SpawnAsteroid();
      _timer += secondsBetweenAsteroids;
    }
  }

  void SpawnAsteroid()
  {
    Vector2 spawnPoint = Vector2.zero;
    Vector2 direction = Vector2.zero;
    int side = Random.Range(0, 4);
    switch (side)
    {
      case 0:
        //* Left
        spawnPoint = new Vector2(0, Random.value);
        direction = new Vector2(1f, Random.Range(-1f, 1f));
        break;
      case 1:
        //* Right
        spawnPoint = new Vector2(1, Random.value);
        direction = new Vector2(-1f, Random.Range(-1f, 1f));
        break;
      case 2:
        //* Bottom
        spawnPoint = new Vector2(Random.value, 0);
        direction = new Vector2(Random.Range(-1f, 1f), 1f);
        break;
      case 3:
        //* Top
        spawnPoint = new Vector2(Random.value, 1);
        direction = new Vector2(Random.Range(-1f, 1f), -1f);
        break;
    }
    Vector3 worldSpawnPoint = _mainCamera.ViewportToWorldPoint(spawnPoint);
    worldSpawnPoint.z = 0;
    GameObject asteroid = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];
    GameObject instance = Instantiate(
        asteroid,
        worldSpawnPoint,
        Quaternion.Euler(0f, 0f, Random.Range(9f, 360f)));
    Rigidbody rb = instance.GetComponent<Rigidbody>();
    rb.velocity = direction.normalized * Random.Range(forceRange.x, forceRange.y);
  }
}

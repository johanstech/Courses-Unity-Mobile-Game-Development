using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class BallHandler : MonoBehaviour
{
  [SerializeField]
  GameObject ballPrefab;
  [SerializeField]
  Rigidbody2D pivot;
  [SerializeField]
  float detachDelay = 0.25f;
  [SerializeField]
  float respawnDelay = 0.75f;

  Rigidbody2D _currentBallRigidbody;
  SpringJoint2D _currentBallSpringJoint;
  Camera _mainCamera;
  bool _isDragging;

  void Awake()
  {
    _mainCamera = Camera.main;
    SpawnNewBall();
  }

  void OnEnable()
  {
    EnhancedTouchSupport.Enable();
  }

  void OnDisable()
  {
    EnhancedTouchSupport.Disable();
  }

  void Update()
  {
    if (_currentBallRigidbody == null) return;

    HandleBall();
  }

  void HandleBall()
  {
    // if (!Touchscreen.current.primaryTouch.press.isPressed)
    if (Touch.activeTouches.Count == 0)
    {
      if (_isDragging)
      {
        LaunchBall();
      }

      _isDragging = false;
      return;
    }

    _isDragging = true;
    _currentBallRigidbody.isKinematic = true;
    Vector2 touchPosition = new Vector2();
    foreach (Touch touch in Touch.activeTouches)
    {
      touchPosition += touch.screenPosition;
    }
    touchPosition /= Touch.activeTouches.Count;
    // Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
    Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(touchPosition);
    _currentBallRigidbody.position = worldPosition;
  }

  void SpawnNewBall()
  {
    GameObject ballInstance = Instantiate(ballPrefab, pivot.position, Quaternion.identity);
    _currentBallRigidbody = ballInstance.GetComponent<Rigidbody2D>();
    _currentBallSpringJoint = ballInstance.GetComponent<SpringJoint2D>();
    _currentBallSpringJoint.connectedBody = pivot;
  }

  void LaunchBall()
  {
    _currentBallRigidbody.isKinematic = false;
    _currentBallRigidbody = null;

    Invoke(nameof(DetachBall), detachDelay);
  }

  void DetachBall()
  {
    _currentBallSpringJoint.enabled = false;
    _currentBallSpringJoint = null;

    Invoke(nameof(SpawnNewBall), respawnDelay);
  }
}

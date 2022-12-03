using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
  [SerializeField]
  Rigidbody2D currentBallRigidbody;

  Camera _mainCamera;

  void Awake()
  {
    _mainCamera = Camera.main;
  }

  void Update()
  {
    if (!Touchscreen.current.primaryTouch.press.isPressed)
    {
      currentBallRigidbody.isKinematic = false;
      return;
    }

    currentBallRigidbody.isKinematic = true;
    Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
    Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(touchPosition);
    currentBallRigidbody.position = worldPosition;
  }
}

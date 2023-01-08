using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
  [SerializeField]
  float forceMagnitude;
  [SerializeField]
  float maxVelocity;

  Rigidbody _rigidBody;
  Camera _mainCamera;
  Vector3 moveDirection;

  void Start()
  {
    _rigidBody = GetComponent<Rigidbody>();
    _mainCamera = Camera.main;
  }

  void Update()
  {
    ProcessInput();
    KeepPlayerOnScreen();
  }

  void FixedUpdate()
  {
    if (moveDirection == Vector3.zero) return;

    _rigidBody.AddForce(moveDirection * forceMagnitude, ForceMode.Force);
    _rigidBody.velocity = Vector3.ClampMagnitude(_rigidBody.velocity, maxVelocity);
  }

  void ProcessInput()
  {
    if (!Touchscreen.current.primaryTouch.press.isPressed)
    {
      moveDirection = Vector3.zero;
      return;
    }

    Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
    Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(touchPosition);
    moveDirection = transform.position - worldPosition;
    moveDirection.z = 0;
    moveDirection.Normalize();
  }

  void KeepPlayerOnScreen()
  {
    Vector3 newPosition = transform.position;
    Vector3 viewportPosition = _mainCamera.WorldToViewportPoint(newPosition);

    if (viewportPosition.x > 1)
    {
      newPosition.x = -newPosition.x + 0.1f;
    }
    else if (viewportPosition.x < 0)
    {
      newPosition.x = -newPosition.x - 0.1f;
    }
    else if (viewportPosition.y > 1)
    {
      newPosition.y = -newPosition.y + 0.1f;
    }
    else if (viewportPosition.y < 0)
    {
      newPosition.y = -newPosition.y - 0.1f;
    }

    transform.position = newPosition;
  }
}

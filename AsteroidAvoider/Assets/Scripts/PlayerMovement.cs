using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
  [SerializeField]
  float forceMagnitude;
  [SerializeField]
  float maxVelocity;
  [SerializeField]
  float rotationSpeed;

  Rigidbody _rb;
  Camera _mainCamera;
  Vector3 moveDirection;

  void Start()
  {
    _rb = GetComponent<Rigidbody>();
    _mainCamera = Camera.main;
  }

  void Update()
  {
    ProcessInput();
    KeepPlayerOnScreen();
    RotateToFaceVelocity();
  }

  void FixedUpdate()
  {
    if (moveDirection == Vector3.zero) return;

    _rb.AddForce(moveDirection * forceMagnitude, ForceMode.Force);
    _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, maxVelocity);
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

  void RotateToFaceVelocity()
  {
    if (_rb.velocity == Vector3.zero) return;

    Quaternion targetRotation = Quaternion.LookRotation(_rb.velocity, Vector3.back);
    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
  }
}

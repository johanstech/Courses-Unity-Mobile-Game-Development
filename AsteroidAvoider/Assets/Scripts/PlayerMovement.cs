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
    Move();
  }

  void FixedUpdate()
  {
    if (moveDirection == Vector3.zero) return;

    _rigidBody.AddForce(moveDirection * forceMagnitude, ForceMode.Force);
    _rigidBody.velocity = Vector3.ClampMagnitude(_rigidBody.velocity, maxVelocity);
  }

  void Move()
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
}

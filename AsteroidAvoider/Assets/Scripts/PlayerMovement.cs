using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
  Camera _mainCamera;

  void Start()
  {
    _mainCamera = Camera.main;
  }

  void Update()
  {
    if (!Touchscreen.current.primaryTouch.press.isPressed) return;

    Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
    Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(touchPosition);
  }
}

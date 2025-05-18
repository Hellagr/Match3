using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    InputSystem_Actions action;

    public event Action<Vector2> ObjectAddedToBar;

    void Awake()
    {
        action = new InputSystem_Actions();
    }

    void OnEnable()
    {
        action.UI.Point.performed += Point;
        action.Enable();
    }

    private void Point(InputAction.CallbackContext context)
    {
        Vector2 point;

        if (Touchscreen.current != null)
        {
            point = Touchscreen.current.primaryTouch.position.ReadValue();
        }
        else
        {
            point = context.ReadValue<Vector2>();
        }

        ObjectAddedToBar?.Invoke(point);
    }

    void OnDisable()
    {
        action.UI.Point.performed -= Point;
        action.Disable();
    }
}

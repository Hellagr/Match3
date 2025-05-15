using System.Drawing;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    InputSystem_Actions action;
    Vector2 point;

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
        point = context.ReadValue<Vector2>();

        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(point);

        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
        if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Figure"))
        {
            var parent = hit.collider.gameObject.transform.parent;
            parent.parent.gameObject.SetActive(false);
        }
    }

    void OnDisable()
    {
        action.UI.Point.performed -= Point;
        action.Disable();
    }
}

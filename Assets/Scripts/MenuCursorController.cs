using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class MenuCursorController : MonoBehaviour
{
    [SerializeField] private InputSystemUIInputModule uiInputModule;
    [SerializeField] private float mouseMovementThreshold = 2f;

    private InputAction navigateAction;

    private void OnEnable()
    {
        navigateAction = uiInputModule.move?.action;

        if (navigateAction != null)
        {
            navigateAction.performed += OnNavigate;
        }
    }

    private void OnDisable()
    {
        if (navigateAction != null)
        {
            navigateAction.performed -= OnNavigate;
        }
    }

    private void OnNavigate(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();

        if (direction.sqrMagnitude > 0.01f)
        {
            Cursor.visible = false;
        }
    }

    private void Update()
    {
        if (Mouse.current == null)
            return;

        Vector2 movement = Mouse.current.delta.ReadValue();

        if (movement.sqrMagnitude >
            mouseMovementThreshold * mouseMovementThreshold)
        {
            Cursor.visible = true;
        }
    }
}

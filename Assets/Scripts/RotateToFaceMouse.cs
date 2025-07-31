using UnityEngine;
using UnityEngine.InputSystem;

//https://www.youtube.com/watch?v=RZ_WDThJUb4
public class RotateToFaceMouse : MonoBehaviour
{
    [SerializeField] private Camera mainCamera; // Reference to the main camera
    private Vector2 mousePosition; // Position of the mouse in world space

    public void OnLook(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // Get the mouse position in world space
            mousePosition = context.ReadValue<Vector2>();
            RotateTowardsMouse();
        }
    }
    private void RotateTowardsMouse()
    {
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, mainCamera.nearClipPlane));

        Vector3 rotateDirection = (worldPosition - transform.position).normalized;
        rotateDirection.z = 0;

        float angle = Mathf.Atan2(rotateDirection.y, rotateDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}

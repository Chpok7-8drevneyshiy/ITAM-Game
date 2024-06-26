using UnityEngine;

public class PlayerScreenPosition : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 initialViewportPosition;
    private Vector2 initialScreenSize;

    private void Awake()
    {
        mainCamera = Camera.main;
        initialViewportPosition = mainCamera.WorldToViewportPoint(transform.position);
        initialScreenSize = new Vector2(Screen.width, Screen.height);
    }

    private void OnEnable()
    {
        WindowResizer.OnWindowResize += HandleWindowResize;
    }

    private void OnDisable()
    {
        WindowResizer.OnWindowResize -= HandleWindowResize;
    }

    private void HandleWindowResize(WindowResizeEventArgs e)
    {
        Vector3 newPosition = mainCamera.ViewportToWorldPoint(initialViewportPosition);
        newPosition.z = transform.position.z;
        transform.position = newPosition;
        initialScreenSize = new Vector2(Screen.width, Screen.height);
    }
}

using UnityEngine;

public class ResponsivePosition : MonoBehaviour
{
    private Vector2 defaultResolution = new Vector2(400, 400);
    private Vector2 currentResolution;
    private Vector2 scaleRatio;

    void Start()
    {
        currentResolution = new Vector2(Screen.width, Screen.height);
        scaleRatio = new Vector2(currentResolution.x / defaultResolution.x, currentResolution.y / defaultResolution.y);
        AdjustPosition();
    }

    void Update()
    {
        if (currentResolution.x != Screen.width || currentResolution.y != Screen.height)
        {
            currentResolution = new Vector2(Screen.width, Screen.height);
            scaleRatio = new Vector2(currentResolution.x / defaultResolution.x, currentResolution.y / defaultResolution.y);
            AdjustPosition();
        }
    }

    void AdjustPosition()
    {
        transform.position = new Vector3(transform.position.x * scaleRatio.x, transform.position.y * scaleRatio.y, transform.position.z);
    }
}

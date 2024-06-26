using UnityEngine;

public class EdgeObjectPlacer : MonoBehaviour
{
    public Transform topEdgeTrigger;
    public Transform bottomEdgeTrigger;
    public Transform leftEdgeTrigger;
    public Transform rightEdgeTrigger;

    private Camera mainCamera;
    private Vector2 screenSize;

    void Start()
    {
        mainCamera = Camera.main; 
        UpdateScreenSize();
    }

    void Update()
    {
        if (screenSize.x != Screen.width || screenSize.y != Screen.height)
        {
            UpdateScreenSize();
            UpdateEdgePositionsAndSizes();
        }
    }

    void UpdateScreenSize()
    {
        screenSize = new Vector2(Screen.width, Screen.height);
    }

    void UpdateEdgePositionsAndSizes()
    {
        float top = mainCamera.orthographicSize;
        float bottom = -top;
        float left = bottom * mainCamera.aspect;
        float right = top * mainCamera.aspect;
        topEdgeTrigger.position = new Vector2(0, top);
        topEdgeTrigger.localScale = new Vector3(right * 2, 1, 1); 

        bottomEdgeTrigger.position = new Vector2(0, bottom);
        bottomEdgeTrigger.localScale = new Vector3(right * 2, 1, 1); 

        leftEdgeTrigger.position = new Vector2(left, 0);
        leftEdgeTrigger.localScale = new Vector3(1, top * 2, 1); 

        rightEdgeTrigger.position = new Vector2(right, 0);
        rightEdgeTrigger.localScale = new Vector3(1, top * 2, 1);
    }
}

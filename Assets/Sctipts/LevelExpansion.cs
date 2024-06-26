using UnityEngine;

public class LevelExpansion : MonoBehaviour
{
    public GameObject[] wallSections;
    private int lastWindowWidth;

    void Start()
    {
        lastWindowWidth = Screen.width;
        UpdateWallVisibility();
    }

    void Update()
    {
        if (Screen.width != lastWindowWidth)
        {
            lastWindowWidth = Screen.width;
            UpdateWallVisibility();
        }
    }

    void UpdateWallVisibility()
    {
        foreach (var wall in wallSections)
        {
            if (Screen.width > 800)
            {
                wall.SetActive(true);
            }
            else
            {
                wall.SetActive(false);
            }
        }
    }
}

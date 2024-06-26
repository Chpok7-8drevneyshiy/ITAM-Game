using UnityEngine;
using System;
using System.Runtime.InteropServices;

public class GameWindowMover : MonoBehaviour
{
    [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
    private static extern bool SetWindowPos(IntPtr hwnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int uFlags);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    private IntPtr windowHandle;
    public Transform player;
    public Vector2 margin = new Vector2(10, 10);
    private Vector2 windowSize = new Vector2(400, 400);

    private void Awake()
    {
        windowHandle = FindWindow(null, Application.productName);
        SetWindowPos(windowHandle, 0, (Screen.width - (int)windowSize.x) / 2, (Screen.height - (int)windowSize.y) / 2, (int)windowSize.x, (int)windowSize.y, 0);
    }

    private void Update()
    {
        Vector2 playerScreenPos = Camera.main.WorldToScreenPoint(player.position);

        Vector2 windowPos = new Vector2(
            Mathf.Clamp(playerScreenPos.x - windowSize.x / 2, margin.x, Screen.width - windowSize.x - margin.x),
            Mathf.Clamp(Screen.height - playerScreenPos.y - windowSize.y / 2, margin.y, Screen.height - windowSize.y - margin.y)
        );

        SetWindowPos(windowHandle, 0, (int)windowPos.x, (int)windowPos.y, (int)windowSize.x, (int)windowSize.y, 0);
    }
}

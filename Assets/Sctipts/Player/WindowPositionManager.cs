using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class WindowPositionManager : MonoBehaviour
{
    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

    [StructLayout(LayoutKind.Sequential)]
    private struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    private Vector2 initialPosition;
    private Vector2 initialSize;

    void Start()
    {
        if (TryGetWindowRect(out RECT rect))
        {
            initialPosition = new Vector2(rect.Left, rect.Top);
            initialSize = new Vector2(rect.Right - rect.Left, rect.Bottom - rect.Top);
        }
    }

    void Update()
    {
        if (TryGetWindowRect(out RECT rect))
        {
            Vector2 currentPosition = new Vector2(rect.Left, rect.Top);
            Vector2 currentSize = new Vector2(rect.Right - rect.Left, rect.Bottom - rect.Top);
            if (initialPosition != currentPosition || initialSize != currentSize)
            {
                Vector2 deltaPosition = currentPosition - initialPosition;
                Vector2 deltaSize = currentSize - initialSize;
                transform.position += new Vector3(deltaPosition.x, deltaPosition.y, 0);
                initialPosition = currentPosition;
                initialSize = currentSize;
            }
        }
    }

    private bool TryGetWindowRect(out RECT rect)
    {
        rect = new RECT();
        IntPtr hwnd = GetActiveWindow();
        if (hwnd == IntPtr.Zero)
        {
            Debug.LogError("Не удается найти окно.");
            return false;
        }
        return GetWindowRect(hwnd, out rect);
    }
}

using UnityEngine;
using System;
using System.Runtime.InteropServices;

public class WindowAutoResizer : MonoBehaviour
{
    public float shrinkRate = 1f;
    private float timer = 0f;
    private const int minWidth = 400;
    private const int minHeight = 400;

    [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
    private static extern bool SetWindowPos(IntPtr hwnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    private const int SWP_NOZORDER = 0X4;
    private const int SWP_SHOWWINDOW = 0x0040;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > shrinkRate)
        {
            ResizeAndCenterWindow(-2, -2);
            timer = 0f;
        }
    }

    private void ResizeAndCenterWindow(int widthChange, int heightChange)
    {
        IntPtr windowHandle = FindWindow(null, Application.productName);
        if (windowHandle == IntPtr.Zero)
        {
            Debug.LogError("Окно не найдено");
            return;
        }

        Rect currentWindowRect = new Rect();
        GetWindowRect(windowHandle, ref currentWindowRect);

        int newWidth = Mathf.Max(minWidth, currentWindowRect.width + widthChange);
        int newHeight = Mathf.Max(minHeight, currentWindowRect.height + heightChange);
        int newX = currentWindowRect.left;
        int newY = currentWindowRect.top;
        if (currentWindowRect.width > minWidth)
        {
            newX = currentWindowRect.left - widthChange / 2;
        }
        if (currentWindowRect.height > minHeight)
        {
            newY = currentWindowRect.top - heightChange / 2;
        }

        SetWindowPos(windowHandle, 0, newX, newY, newWidth, newHeight, SWP_NOZORDER | SWP_SHOWWINDOW);
    }

    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hwnd, ref Rect rect);

    [StructLayout(LayoutKind.Sequential)]
    public struct Rect
    {
        public int left;
        public int top;
        public int right;
        public int bottom;

        public int width { get { return right - left; } }
        public int height { get { return bottom - top; } }
    }
}

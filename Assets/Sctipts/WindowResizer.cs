using System;
using UnityEngine;
using System.Runtime.InteropServices;

public class WindowResizer : MonoBehaviour
{
    public static event Action<WindowResizeEventArgs> OnWindowResize;

    [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
    private static extern bool SetWindowPos(IntPtr hwnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hwnd, ref Rect rect);

    private const int SWP_NOZORDER = 0X4;
    private const int SWP_SHOWWINDOW = 0x0040;

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

    public void ResizeAndShiftWindow(int widthChange, int heightChange)
    {
        IntPtr windowHandle = FindWindow(null, Application.productName);
        if (windowHandle == IntPtr.Zero)
        {
            Debug.LogError("Окно не найдено");
            return;
        }

        Rect currentWindowRect = new Rect();
        GetWindowRect(windowHandle, ref currentWindowRect);
        int newX = currentWindowRect.left;
        int newY = currentWindowRect.top;
        if (widthChange < 0)
        {
            newX += widthChange; 
            widthChange = -widthChange; 
        }
        if (heightChange < 0)
        {
            newY += heightChange;
            heightChange = -heightChange; 
        }

        int newWidth = currentWindowRect.width + widthChange;
        int newHeight = currentWindowRect.height + heightChange;
        bool success = SetWindowPos(windowHandle, 0, newX, newY, newWidth, newHeight, SWP_NOZORDER | SWP_SHOWWINDOW);

        if (!success)
        {
            Debug.LogError("Не удалось изменить позицию и размер окна");
            return;
        }
        OnWindowResize?.Invoke(new WindowResizeEventArgs
        {
            WidthChange = widthChange,
            HeightChange = heightChange
        });
    }
    public void Up()
    {
        ResizeAndShiftWindow(0, -100);
    }
    public void Right()
    {
        ResizeAndShiftWindow(100, 0);
    }
    public void Left()
    {
        ResizeAndShiftWindow(-100, 0);
    }
    public void Down()
    {
        ResizeAndShiftWindow(0, 100);
    }
}
public class WindowResizeEventArgs : EventArgs
{
    public int WidthChange { get; set; }
    public int HeightChange { get; set; }
}

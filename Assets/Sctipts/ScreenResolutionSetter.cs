using UnityEngine;
using System.Runtime.InteropServices;
using System;

public class ScreenResolutionSetter : MonoBehaviour
{
    [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
    private static extern bool SetWindowPos(IntPtr hwnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

    [DllImport("user32.dll")]
    static extern int GetSystemMetrics(int nIndex);

    private const int SM_CXSCREEN = 0;
    private const int SM_CYSCREEN = 1;
    private const int SWP_NOZORDER = 0X4;
    private const int SWP_SHOWWINDOW = 0x0040;
    [SerializeField] private int Width;
    [SerializeField] private int Height;
    [SerializeField] private bool NeedChangeResolution;
    void Awake()
    {
        Resolution();
        CenterWindow();
    }
    private void Update()
    {
      //  Resolution();
    }
    private void Resolution()
    {
        if (NeedChangeResolution)
        {
            int newWidth = Width;
            int newHeight = Height;
            Screen.SetResolution(newWidth, newHeight, false);
        }
    }
    void CenterWindow()
{
    IntPtr windowHandle = FindWindow(null, Application.productName);

    if (windowHandle == IntPtr.Zero)
    {
        Debug.LogError("Окно не найдено");
        return;
    }

    int screenWidth = GetSystemMetrics(SM_CXSCREEN);
    int screenHeight = GetSystemMetrics(SM_CYSCREEN);

    RECT rect;
    if (GetWindowRect(windowHandle, out rect))
    {
        int windowWidth = rect.Right - rect.Left;
        int windowHeight = rect.Bottom - rect.Top;

        int posX = (screenWidth - windowWidth) / 2;
        int posY = (screenHeight - windowHeight) / 2;

        SetWindowPos(windowHandle, (int)IntPtr.Zero, posX, posY, windowWidth, windowHeight, SWP_NOZORDER | SWP_SHOWWINDOW);
    }
}

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }
}

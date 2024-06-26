using System;
using UnityEngine;
using System.Runtime.InteropServices;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float windowMoveSpeed = 10f;
    private Animator animator;
    private Rigidbody2D _rigidbody;
    private Vector2 direction;

    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    [DllImport("user32.dll")]
    private static extern bool SetWindowPos(IntPtr hwnd, int hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);

    private const int SWP_NOSIZE = 0x0001;
    private const int SWP_NOZORDER = 0x0004;
    private const int SWP_SHOWWINDOW = 0x0040;

    private IntPtr windowHandle;
    private Vector2 initialWindowPosition;

    private void Start()
    {
        animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        windowHandle = FindWindow(null, Application.productName);
        GetWindowPosition(out initialWindowPosition);
    }

    private void FixedUpdate()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
        if(direction.y > 0)
        {
            animator.SetInteger("State", 2);
        }
        else
        if (direction.y < 0)
        {
            animator.SetInteger("State", 3);
        }
        else
        if (direction.x != 0)
        {
            animator.SetInteger("State", 1);
        }
        else
        {
            animator.SetInteger("State", 0);
        }
        if(direction.x < 0)
        {
            transform.localScale = new Vector3(1,1,1);
        }
        else
            transform.localScale = new Vector3(-1, 1,1);

        _rigidbody.MovePosition(_rigidbody.position + direction * speed * Time.fixedDeltaTime);
        if (direction != Vector2.zero)
        {
            MoveWindow((int)(direction.x * windowMoveSpeed), (int)(-direction.y * windowMoveSpeed));
        }
    }

    private void MoveWindow(int offsetX, int offsetY)
    {
        GetWindowPosition(out Vector2 currentWindowPosition);
        int newX = (int)currentWindowPosition.x + offsetX;
        int newY = (int)currentWindowPosition.y + offsetY;
        SetWindowPos(windowHandle, (int)IntPtr.Zero, newX, newY, 0, 0, SWP_NOSIZE | SWP_NOZORDER | SWP_SHOWWINDOW);
    }

    private void GetWindowPosition(out Vector2 position)
    {
        RECT rect;
        GetWindowRect(windowHandle, out rect);
        position = new Vector2(rect.Left, rect.Top);
    }
    private struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
}

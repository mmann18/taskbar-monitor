using System.Runtime.InteropServices;
using Avalonia.Controls;
using Avalonia.Platform;

namespace TaskbarMonitorAvaloniaDemo.Interop;

internal static class TaskbarHost
{
    private const int GwlStyle = -16;
    private const long WsChild = 0x40000000L;
    private const long WsVisible = 0x10000000L;
    private const long WsPopup = 0x80000000L;

    public static void AttachToTaskbar(Window window)
    {
        var platformHandle = window.TryGetPlatformHandle();
        if (platformHandle is null)
        {
            return;
        }

        var taskbarHandle = FindWindow("Shell_TrayWnd", null);
        if (taskbarHandle == IntPtr.Zero)
        {
            return;
        }

        var windowHandle = platformHandle.Handle;
        var style = GetWindowLongPtr(windowHandle, GwlStyle).ToInt64();
        style &= ~WsPopup;
        style |= WsChild | WsVisible;
        SetWindowLongPtr(windowHandle, GwlStyle, new IntPtr(style));
        SetParent(windowHandle, taskbarHandle);

        PositionWindow(window, windowHandle, taskbarHandle);
    }

    private static void PositionWindow(Window window, IntPtr windowHandle, IntPtr taskbarHandle)
    {
        if (!GetWindowRect(taskbarHandle, out var taskbarRect))
        {
            return;
        }

        var width = Math.Max(1, (int)Math.Round(window.Bounds.Width));
        var height = Math.Max(1, (int)Math.Round(window.Bounds.Height));

        var taskbarWidth = taskbarRect.Right - taskbarRect.Left;
        var taskbarHeight = taskbarRect.Bottom - taskbarRect.Top;

        int x;
        int y;

        if (taskbarWidth >= taskbarHeight)
        {
            x = taskbarRect.Right - width - 8;
            y = taskbarRect.Top + (taskbarHeight - height) / 2;
        }
        else
        {
            x = taskbarRect.Left + (taskbarWidth - width) / 2;
            y = taskbarRect.Bottom - height - 8;
        }

        MoveWindow(windowHandle, x, y, width, height, true);
    }

    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern IntPtr FindWindow(string lpClassName, string? lpWindowName);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool bRepaint);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool GetWindowRect(IntPtr hwnd, out Rect lpRect);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

    [StructLayout(LayoutKind.Sequential)]
    private readonly struct Rect
    {
        public readonly int Left;
        public readonly int Top;
        public readonly int Right;
        public readonly int Bottom;
    }
}

// <copyright file="NativeMethods.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>

namespace JeremyAnsel.DirectX.Window
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Text;

    [SecurityCritical, SuppressUnmanagedCodeSecurity]
    internal static class NativeMethods
    {
        public const int DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE = -3;
        public const int PROCESS_PER_MONITOR_DPI_AWARE = 2;

        [DllImport("user32.dll", EntryPoint = "CreateWindowExW")]
        public static extern IntPtr CreateWindowEx(
            int styleEx,
            IntPtr classAtom,
            [MarshalAs(UnmanagedType.LPWStr)] string windowName,
            WindowStyles style,
            int x,
            int y,
            int width,
            int height,
            IntPtr parentHandle,
            IntPtr menu,
            IntPtr instance,
            IntPtr param);

        [DllImport("user32.dll", EntryPoint = "DefWindowProcW")]
        public static extern IntPtr DefWindowProc(IntPtr hWnd, WindowMessageType msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "DestroyWindow")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DestroyWindow(IntPtr handle);

        [DllImport("user32.dll", EntryPoint = "DispatchMessageW")]
        public static extern IntPtr DispatchMessage([In] ref WindowMessage message);

        [DllImport("user32.dll", EntryPoint = "GetClientRect")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetClientRect(IntPtr handle, [Out] IntPtr rect);

        [DllImport("user32.dll", EntryPoint = "GetMessageW")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetMessage([Out] out WindowMessage message, IntPtr handle, WindowMessageType filterMin, WindowMessageType filterMax);

        [DllImport("user32.dll", EntryPoint = "GetWindowTextW")]
        public static extern int GetWindowText(IntPtr handle, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder text, int maxCount);

        [DllImport("user32.dll", EntryPoint = "GetWindowTextLengthW")]
        public static extern int GetWindowTextLength(IntPtr handle);

        [DllImport("user32.dll", EntryPoint = "IsProcessDPIAware")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsProcessDPIAware();

        [DllImport("user32.dll", EntryPoint = "LoadIconW")]
        public static extern IntPtr LoadIcon(IntPtr instance, IntPtr name);

        [DllImport("user32.dll", EntryPoint = "LoadCursorW")]
        public static extern IntPtr LoadCursor(IntPtr instance, IntPtr name);

        [SuppressMessage("Microsoft.Usage", "CA2205:UseManagedEquivalentsOfWin32Api", Justification = "Reviewed")]
        [DllImport("user32.dll", EntryPoint = "MessageBoxW", CharSet = CharSet.Unicode)]
        public static extern MessageBoxResult MessageBox(IntPtr handle, string text, string caption, uint type);

        [DllImport("user32.dll", EntryPoint = "PeekMessageW")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PeekMessage([Out] out WindowMessage message, IntPtr handle, WindowMessageType filterMin, WindowMessageType filterMax, uint remove);

        [DllImport("user32.dll", EntryPoint = "PostQuitMessage")]
        public static extern void PostQuitMessage(int nExitCode);

        [DllImport("user32.dll", EntryPoint = "RegisterClassExW")]
        public static extern ushort RegisterClassEx(ref ClassInfoEx windowClass);

        [DllImport("user32.dll", EntryPoint = "SetFocus")]
        public static extern IntPtr SetFocus(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "SetProcessDPIAware")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetProcessDPIAware();

        [DllImport("shcore.dll", EntryPoint = "SetProcessDpiAwareness")]
        public static extern IntPtr SetProcessDpiAwareness(int value);

        [DllImport("user32.dll", EntryPoint = "SetProcessDpiAwarenessContext")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetProcessDpiAwarenessContext(int value);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongW")]
        private static extern int SetWindowLong32(IntPtr handle, int index, int value);

        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist", Justification = "Reviewed")]
        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtrW")]
        private static extern long SetWindowLong64(IntPtr handle, int index, long value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntPtr SetWindowLongPtr(IntPtr handle, int index, IntPtr value)
        {
            if (Environment.Is64BitProcess)
            {
                return new IntPtr(SetWindowLong64(handle, index, value.ToInt64()));
            }
            else
            {
                return new IntPtr(SetWindowLong32(handle, index, value.ToInt32()));
            }
        }

        [DllImport("user32.dll", EntryPoint = "SetWindowTextW")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowText(IntPtr handle, [MarshalAs(UnmanagedType.LPWStr)] String text);

        [DllImport("user32.dll", EntryPoint = "ShowWindow")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowWindow(IntPtr handle, ShowWindow cmdShow);

        [DllImport("user32.dll", EntryPoint = "TranslateMessage")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool TranslateMessage([In] ref WindowMessage message);

        [DllImport("user32.dll", EntryPoint = "UnregisterClassW")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnregisterClass(IntPtr atom, IntPtr instance);
    }
}

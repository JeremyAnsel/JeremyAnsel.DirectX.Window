// <copyright file="NativeMethods.cs" company="Jérémy Ansel">
// Copyright (c) 2015-2026 Jérémy Ansel
// </copyright>

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace JeremyAnsel.DirectX.Window
{
    [SecurityCritical, SuppressUnmanagedCodeSecurity]
    internal static unsafe partial class NativeMethods
    {
        public const int DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE = -3;
        public const int PROCESS_PER_MONITOR_DPI_AWARE = 2;

#if NET8_0_OR_GREATER
        [LibraryImport("user32.dll", EntryPoint = "CreateWindowExW")]
        public static partial IntPtr CreateWindowEx(
#else
        [DllImport("user32.dll", EntryPoint = "CreateWindowExW")]
        public static extern IntPtr CreateWindowEx(
#endif
            int styleEx,
            IntPtr classAtom,
            char* windowName,
            WindowStyles style,
            int x,
            int y,
            int width,
            int height,
            IntPtr parentHandle,
            IntPtr menu,
            IntPtr instance,
            IntPtr param);

#if NET8_0_OR_GREATER
        [LibraryImport("user32.dll", EntryPoint = "DefWindowProcW")]
        public static partial IntPtr DefWindowProc(
#else
        [DllImport("user32.dll", EntryPoint = "DefWindowProcW")]
        public static extern IntPtr DefWindowProc(
#endif
            IntPtr hWnd, WindowMessageType msg, IntPtr wParam, IntPtr lParam);

#if NET8_0_OR_GREATER
        [LibraryImport("user32.dll", EntryPoint = "DestroyIcon")]
        public static partial int DestroyIcon(
#else
        [DllImport("user32.dll", EntryPoint = "DestroyIcon")]
        public static extern int DestroyIcon(
#endif
            IntPtr handle);

#if NET8_0_OR_GREATER
        [LibraryImport("user32.dll", EntryPoint = "DestroyWindow")]
        public static partial int DestroyWindow(
#else
        [DllImport("user32.dll", EntryPoint = "DestroyWindow")]
        public static extern int DestroyWindow(
#endif
            IntPtr handle);

#if NET8_0_OR_GREATER
        [LibraryImport("user32.dll", EntryPoint = "DispatchMessageW")]
        public static partial IntPtr DispatchMessage(
#else
        [DllImport("user32.dll", EntryPoint = "DispatchMessageW")]
        public static extern IntPtr DispatchMessage(
#endif
            ref WindowMessage message);

#if NET8_0_OR_GREATER
        [LibraryImport("shell32.dll", EntryPoint = "ExtractAssociatedIconW")]
        private static partial IntPtr ExtractAssociatedIconW(
#else
        [DllImport("shell32.dll", EntryPoint = "ExtractAssociatedIconW")]
        private static extern IntPtr ExtractAssociatedIconW(
#endif
            IntPtr instance, char* path, ref short index);

        public static IntPtr ExtractAssociatedIcon(IntPtr instance, string path, short index)
        {
            char* buffer = stackalloc char[260];

            fixed (char* ptr = path)
            {
                for (int i = 0; i < path.Length; i++)
                {
                    buffer[i] = ptr[i];
                }

                buffer[path.Length] = '\0';
            }

            return ExtractAssociatedIconW(instance, buffer, ref index);
        }

#if NET8_0_OR_GREATER
        [LibraryImport("user32.dll", EntryPoint = "GetClientRect")]
        public static partial int GetClientRect(
#else
        [DllImport("user32.dll", EntryPoint = "GetClientRect")]
        public static extern int GetClientRect(
#endif
            IntPtr handle, IntPtr rect);

#if NET8_0_OR_GREATER
        [LibraryImport("user32.dll", EntryPoint = "GetMessageW")]
        public static partial int GetMessage(
#else
        [DllImport("user32.dll", EntryPoint = "GetMessageW")]
        public static extern int GetMessage(
#endif
            out WindowMessage message, IntPtr handle, WindowMessageType filterMin, WindowMessageType filterMax);

#if NET8_0_OR_GREATER
        [LibraryImport("user32.dll", EntryPoint = "GetWindowTextW")]
        public static partial int GetWindowText(
#else
        [DllImport("user32.dll", EntryPoint = "GetWindowTextW")]
        public static extern int GetWindowText(
#endif
            IntPtr handle, char* text, int maxCount);

#if NET8_0_OR_GREATER
        [LibraryImport("user32.dll", EntryPoint = "GetWindowTextLengthW")]
        public static partial int GetWindowTextLength(
#else
        [DllImport("user32.dll", EntryPoint = "GetWindowTextLengthW")]
        public static extern int GetWindowTextLength(
#endif
            IntPtr handle);

#if NET8_0_OR_GREATER
        [LibraryImport("user32.dll", EntryPoint = "IsProcessDPIAware")]
        public static partial int IsProcessDPIAware();
#else
        [DllImport("user32.dll", EntryPoint = "IsProcessDPIAware")]
        public static extern int IsProcessDPIAware();
#endif

#if NET8_0_OR_GREATER
        [LibraryImport("user32.dll", EntryPoint = "LoadIconW")]
        public static partial IntPtr LoadIcon(
#else
        [DllImport("user32.dll", EntryPoint = "LoadIconW")]
        public static extern IntPtr LoadIcon(
#endif
            IntPtr instance, IntPtr name);

#if NET8_0_OR_GREATER
        [LibraryImport("user32.dll", EntryPoint = "LoadCursorW")]
        public static partial IntPtr LoadCursor(
#else
        [DllImport("user32.dll", EntryPoint = "LoadCursorW")]
        public static extern IntPtr LoadCursor(
#endif
            IntPtr instance, IntPtr name);

        [SuppressMessage("Microsoft.Usage", "CA2205:UseManagedEquivalentsOfWin32Api", Justification = "Reviewed")]
#if NET8_0_OR_GREATER
        [LibraryImport("user32.dll", EntryPoint = "MessageBoxW")]
        public static partial MessageBoxResult MessageBox(
#else
        [DllImport("user32.dll", EntryPoint = "MessageBoxW")]
        public static extern MessageBoxResult MessageBox(
#endif
            IntPtr handle, char* text, char* caption, uint type);

#if NET8_0_OR_GREATER
        [LibraryImport("user32.dll", EntryPoint = "PeekMessageW")]
        public static partial int PeekMessage(
#else
        [DllImport("user32.dll", EntryPoint = "PeekMessageW")]
        public static extern int PeekMessage(
#endif
            out WindowMessage message, IntPtr handle, WindowMessageType filterMin, WindowMessageType filterMax, uint remove);

#if NET8_0_OR_GREATER
        [LibraryImport("user32.dll", EntryPoint = "PostQuitMessage")]
        public static partial void PostQuitMessage(
#else
        [DllImport("user32.dll", EntryPoint = "PostQuitMessage")]
        public static extern void PostQuitMessage(
#endif
            int nExitCode);

#if NET8_0_OR_GREATER
        [LibraryImport("user32.dll", EntryPoint = "RegisterClassExW")]
        public static partial ushort RegisterClassEx(
#else
        [DllImport("user32.dll", EntryPoint = "RegisterClassExW")]
        public static extern ushort RegisterClassEx(
#endif
            ref ClassInfoEx windowClass);

#if NET8_0_OR_GREATER
        [LibraryImport("user32.dll", EntryPoint = "SetFocus")]
        public static partial IntPtr SetFocus(
#else
        [DllImport("user32.dll", EntryPoint = "SetFocus")]
        public static extern IntPtr SetFocus(
#endif
            IntPtr hWnd);

#if NET8_0_OR_GREATER
        [LibraryImport("user32.dll", EntryPoint = "SetProcessDPIAware")]
        public static partial int SetProcessDPIAware();
#else
        [DllImport("user32.dll", EntryPoint = "SetProcessDPIAware")]
        public static extern int SetProcessDPIAware();
#endif

#if NET8_0_OR_GREATER
        [LibraryImport("shcore.dll", EntryPoint = "SetProcessDpiAwareness")]
        public static partial IntPtr SetProcessDpiAwareness(
#else
        [DllImport("shcore.dll", EntryPoint = "SetProcessDpiAwareness")]
        public static extern IntPtr SetProcessDpiAwareness(
#endif
            int value);

#if NET8_0_OR_GREATER
        [LibraryImport("user32.dll", EntryPoint = "SetProcessDpiAwarenessContext")]
        public static partial int SetProcessDpiAwarenessContext(
#else
        [DllImport("user32.dll", EntryPoint = "SetProcessDpiAwarenessContext")]
        public static extern int SetProcessDpiAwarenessContext(
#endif
            int value);

#if NET8_0_OR_GREATER
        [LibraryImport("user32.dll", EntryPoint = "SetWindowLongW")]
        private static partial int SetWindowLong32(
#else
        [DllImport("user32.dll", EntryPoint = "SetWindowLongW")]
        private static extern int SetWindowLong32(
#endif
            IntPtr handle, int index, int value);

        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist", Justification = "Reviewed")]
#if NET8_0_OR_GREATER
        [LibraryImport("user32.dll", EntryPoint = "SetWindowLongPtrW")]
        private static partial long SetWindowLong64(
#else
        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtrW")]
        private static extern long SetWindowLong64(
#endif
            IntPtr handle, int index, long value);

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

#if NET8_0_OR_GREATER
        [LibraryImport("user32.dll", EntryPoint = "SetWindowTextW")]
        public static partial int SetWindowText(
#else
        [DllImport("user32.dll", EntryPoint = "SetWindowTextW")]
        public static extern int SetWindowText(
#endif
            IntPtr handle, char* text);

#if NET8_0_OR_GREATER
        [LibraryImport("user32.dll", EntryPoint = "ShowWindow")]
        public static partial int ShowWindow(
#else
        [DllImport("user32.dll", EntryPoint = "ShowWindow")]
        public static extern int ShowWindow(
#endif
            IntPtr handle, ShowWindow cmdShow);

#if NET8_0_OR_GREATER
        [LibraryImport("user32.dll", EntryPoint = "TranslateMessage")]
        public static partial int TranslateMessage(
#else
        [DllImport("user32.dll", EntryPoint = "TranslateMessage")]
        public static extern int TranslateMessage(
#endif
            ref WindowMessage message);

#if NET8_0_OR_GREATER
        [LibraryImport("user32.dll", EntryPoint = "UnregisterClassW")]
        public static partial int UnregisterClass(
#else
        [DllImport("user32.dll", EntryPoint = "UnregisterClassW")]
        public static extern int UnregisterClass(
#endif
            IntPtr atom, IntPtr instance);
    }
}

// <copyright file="NativeWindow.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>

namespace JeremyAnsel.DirectX.Window
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;

    [SuppressMessage("Microsoft.Design", "CA1049:TypesThatOwnNativeResourcesShouldBeDisposable", Justification = "Reviewed")]
    internal sealed class NativeWindow
    {
        private delegate IntPtr WindowProcedure(IntPtr hWnd, WindowMessageType msg, IntPtr wParam, IntPtr lParam);

        private readonly WindowBase window;

        [SuppressMessage("Microsoft.Reliability", "CA2006:UseSafeHandleToEncapsulateNativeResources", Justification = "Reviewed")]
        private IntPtr handle;

        private WindowProcedure windowProcedure;

        private bool isDestroyed = false;

        private bool isInitialized = false;

        private bool doPostQuitMessage = false;

        private bool doOnWindowSizeChanged = false;

        private bool doUpdateRender = true;

        public NativeWindow(WindowBase window, string name)
            : this(window, name, WindowConstant.UseDefault, WindowConstant.UseDefault, WindowConstant.UseDefault, WindowConstant.UseDefault, IntPtr.Zero, false)
        {
        }

        public NativeWindow(WindowBase window, string name, int x, int y, int width, int height, IntPtr parentHandle, bool isChild)
        {
            this.window = window;

            this.handle = NativeMethods.CreateWindowEx(
                0,
                NativeClass.Atom,
                name,
                isChild ? WindowStyles.Child : WindowStyles.OverlappedWindow,
                x,
                y,
                width,
                height,
                parentHandle,
                IntPtr.Zero,
                IntPtr.Zero,
                IntPtr.Zero);

            this.PerformanceTime = new WindowPerformanceTime();
        }

        public IntPtr Handle
        {
            get { return this.handle; }
        }

        public string Title
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (this.handle == IntPtr.Zero)
                {
                    return null;
                }

                int length = NativeMethods.GetWindowTextLength(this.handle) + 1;
                StringBuilder sb = new StringBuilder(length);

                if (NativeMethods.GetWindowText(this.handle, sb, length) == 0)
                {
                    return string.Empty;
                }

                return sb.ToString();
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                if (this.handle == IntPtr.Zero)
                {
                    return;
                }

                NativeMethods.SetWindowText(this.handle, value);
            }
        }

        public WindowPerformanceTime PerformanceTime { get; private set; }

        public void Destroy()
        {
            if (!this.isDestroyed)
            {
                NativeMethods.DestroyWindow(this.handle);
                this.handle = IntPtr.Zero;
                this.isDestroyed = true;
            }
        }

        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Reviewed")]
        public void Exit()
        {
            if (this.isDestroyed)
            {
                return;
            }

            this.doPostQuitMessage = true;
        }

        public void Run()
        {
            if (this.isDestroyed)
            {
                return;
            }

            this.isInitialized = false;
            this.doPostQuitMessage = false;
            this.doOnWindowSizeChanged = false;

            this.windowProcedure = new WindowProcedure(this.Callback);
            var windowProcedurePtr = Marshal.GetFunctionPointerForDelegate(this.windowProcedure);
            NativeMethods.SetWindowLongPtr(this.handle, -4, windowProcedurePtr);

            NativeMethods.ShowWindow(this.handle, ShowWindow.ShowDefault);

            this.PerformanceTime.Start();
            this.window.Init();
            this.PerformanceTime.StopEvent();

            WindowMessage message;

            while (true)
            {
                this.PerformanceTime.Tick();

                if (this.doPostQuitMessage)
                {
                    NativeMethods.PostQuitMessage(0);
                    this.doPostQuitMessage = false;
                }

                if (this.doOnWindowSizeChanged)
                {
                    this.PerformanceTime.Start();
                    this.window.OnWindowSizeChanged();
                    this.PerformanceTime.StopEvent();
                    this.doOnWindowSizeChanged = false;

                    if (!this.isInitialized)
                    {
                        this.isInitialized = true;
                    }
                }

                if (this.window.IsMinimized || !this.isInitialized || !this.doUpdateRender || this.isDestroyed)
                {
                    this.PerformanceTime.Start();
                    NativeMethods.GetMessage(out message, IntPtr.Zero, WindowMessageType.Null, WindowMessageType.Null);
                    this.PerformanceTime.StopEvent();
                }
                else
                {
                    if (!NativeMethods.PeekMessage(out message, IntPtr.Zero, WindowMessageType.Null, WindowMessageType.Null, 1))
                    {
                        if (this.window.UpdateWhenInactive || this.window.IsActive)
                        {
                            this.PerformanceTime.Start();
                            this.window.Update();
                            this.PerformanceTime.StopUpdate();
                        }

                        this.PerformanceTime.Start();
                        this.window.Render();
                        this.PerformanceTime.StopRender();

                        this.PerformanceTime.Start();
                        this.window.Present();
                        this.PerformanceTime.StopPresent();

                        continue;
                    }
                }

                if (message.Msg == WindowMessageType.Quit)
                {
                    break;
                }

                this.PerformanceTime.Start();
                NativeMethods.TranslateMessage(ref message);
                NativeMethods.DispatchMessage(ref message);
                this.PerformanceTime.StopEvent();
            }
        }

        private IntPtr Callback(IntPtr hWnd, WindowMessageType msg, IntPtr wParam, IntPtr lParam)
        {
            this.window.OnEvent(msg, wParam, lParam);

            switch (msg)
            {
                case WindowMessageType.Destroy:
                    this.doUpdateRender = false;
                    this.doPostQuitMessage = true;
                    return new IntPtr(1);

                case WindowMessageType.GetMinMaxInfo:
                    Marshal.WriteInt32(lParam, 24, 150);
                    Marshal.WriteInt32(lParam, 28, 150);
                    break;

                case WindowMessageType.Size:
                    this.window.IsMinimized = wParam.ToInt32() == 1;
                    break;

                case WindowMessageType.WindowPositionChanged:
                    {
                        int oldWidth = this.window.Width;
                        int oldHeight = this.window.Height;

                        IntPtr rectPtr = Marshal.AllocHGlobal(16);

                        try
                        {
                            if (NativeMethods.GetClientRect(this.handle, rectPtr))
                            {
                                this.window.Width = Marshal.ReadInt32(rectPtr, 8);
                                this.window.Height = Marshal.ReadInt32(rectPtr, 12);
                            }
                        }
                        finally
                        {
                            Marshal.FreeHGlobal(rectPtr);
                        }

                        if (this.window.Width != oldWidth || this.window.Height != oldHeight)
                        {
                            this.doOnWindowSizeChanged = true;
                        }

                        return NativeMethods.DefWindowProc(hWnd, msg, wParam, lParam);
                    }

                case WindowMessageType.DpiChanged:
                    this.doOnWindowSizeChanged = true;
                    return NativeMethods.DefWindowProc(hWnd, msg, wParam, lParam);

                case WindowMessageType.Activate:
                    this.window.IsActive = (wParam.ToInt32() & 0xFFFF) != 0;
                    break;

                case WindowMessageType.ShowWindow:
                    this.window.IsActive = (wParam.ToInt32()) != 0;
                    break;

                case WindowMessageType.MouseActivate:
                case WindowMessageType.PointerActivate:
                    this.window.Focus();
                    break;

                case WindowMessageType.KeyDown:
                case WindowMessageType.KeyUp:
                    this.window.OnKeyboardEvent((VirtualKey)wParam, (int)((ulong)lParam & 0xFFFFU), ((ulong)lParam & 0x40000000U) != 0, ((ulong)lParam & 0x80000000U) == 0);
                    break;

                default:
                    return NativeMethods.DefWindowProc(hWnd, msg, wParam, lParam);
            }

            return IntPtr.Zero;
        }
    }
}

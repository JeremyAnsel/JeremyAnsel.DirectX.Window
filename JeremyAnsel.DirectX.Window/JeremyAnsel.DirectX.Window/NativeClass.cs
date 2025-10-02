// <copyright file="NativeClass.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>

namespace JeremyAnsel.DirectX.Window
{
    using System;
    using System.Runtime.InteropServices;

    internal sealed class NativeClass : IDisposable
    {
        private delegate IntPtr WindowProcedure(IntPtr hWnd, WindowMessageType msg, IntPtr wParam, IntPtr lParam);

        private IntPtr atom;
        private readonly WindowProcedure windowProcedure;

        private IntPtr icon;

        private static readonly NativeClass WindowClass = new NativeClass();

        public static IntPtr Atom
        {
            get { return NativeClass.WindowClass.atom; }
        }

        private NativeClass()
        {
            this.LoadIcon();

            this.windowProcedure = new WindowProcedure(this.Callback);
            var windowProcedurePtr = Marshal.GetFunctionPointerForDelegate(this.windowProcedure);

            ClassInfoEx windowClass = new ClassInfoEx(
                ClassStyles.HorizontalRedraw | ClassStyles.VerticalRedraw,
                windowProcedurePtr,
                IntPtr.Zero,
                icon != IntPtr.Zero ? icon : NativeMethods.LoadIcon(IntPtr.Zero, new IntPtr(32512)),
                NativeMethods.LoadCursor(IntPtr.Zero, new IntPtr(32512)),
                IntPtr.Zero,
                null,
                typeof(NativeClass).FullName!,
                IntPtr.Zero);

            ushort atom = NativeMethods.RegisterClassEx(ref windowClass);
            this.atom = new IntPtr(atom);
        }

        ~NativeClass()
        {
            this.Dispose(false);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool isDisposing)
        {
            if (this.atom != IntPtr.Zero)
            {
                NativeMethods.UnregisterClass(this.atom, IntPtr.Zero);
                this.atom = IntPtr.Zero;
            }

            if (isDisposing)
            {
                if (this.icon != IntPtr.Zero)
                {
                    NativeMethods.DestroyIcon(this.icon);
                    this.icon = IntPtr.Zero;
                }
            }
        }

        private void LoadIcon()
        {
            this.icon = NativeMethods.ExtractAssociatedIcon(IntPtr.Zero, System.Reflection.Assembly.GetEntryAssembly()!.Location, 0);

            //string iconPath = System.IO.Path.ChangeExtension(AppDomain.CurrentDomain.FriendlyName, ".ico");

            //if (System.IO.File.Exists(iconPath))
            //{
            //    this.icon = new System.Drawing.Icon(iconPath);
            //    return;
            //}

            //iconPath = "Icon.ico";

            //if (System.IO.File.Exists(iconPath))
            //{
            //    this.icon = new System.Drawing.Icon(iconPath);
            //    return;
            //}
        }

        private IntPtr Callback(IntPtr hWnd, WindowMessageType msg, IntPtr wParam, IntPtr lParam)
        {
            switch (msg)
            {
                case WindowMessageType.Destroy:
                    NativeMethods.PostQuitMessage(0);
                    return new IntPtr(1);

                case WindowMessageType.KeyDown:
                    switch ((VirtualKey)wParam)
                    {
                        case VirtualKey.Escape:
                            NativeMethods.PostQuitMessage(0);
                            break;
                    }
                    break;

                default:
                    return NativeMethods.DefWindowProc(hWnd, msg, wParam, lParam);
            }

            return IntPtr.Zero;
        }
    }
}

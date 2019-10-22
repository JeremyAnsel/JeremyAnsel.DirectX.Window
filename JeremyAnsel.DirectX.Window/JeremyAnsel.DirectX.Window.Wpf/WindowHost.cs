// <copyright file="WindowHost.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>

namespace JeremyAnsel.DirectX.Window.Wpf
{
    using System;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using System.Windows.Interop;
    using JeremyAnsel.DirectX.Window;

    public sealed class WindowHost : HwndHost
    {
        private int width;

        private int height;

        public WindowHost(WindowBase window)
            : this(window, 0, 0)
        {
        }

        public WindowHost(WindowBase window, double width, double height)
        {
            if (window == null)
            {
                throw new ArgumentNullException("window");
            }

            if (width < 0)
            {
                throw new ArgumentOutOfRangeException("width");
            }

            if (height < 0)
            {
                throw new ArgumentOutOfRangeException("height");
            }

            this.width = (int)width;
            this.height = (int)height;

            this.Window = window;
        }

        public WindowBase Window { get; private set; }

        protected override HandleRef BuildWindowCore(HandleRef hwndParent)
        {
            this.Window.BuildWindow(0, 0, this.width, this.height, hwndParent.Handle, true);

            Task.Factory.StartNew(() => this.Window.Run());

            return new HandleRef(this, this.Window.Handle);
        }

        protected override void DestroyWindowCore(HandleRef hwnd)
        {
            this.Window.Destroy();
            this.Window = null;
        }

        protected override bool TabIntoCore(TraversalRequest request)
        {
            this.Window.Focus();
            return true;
        }
    }
}

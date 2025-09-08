// <copyright file="WindowHost.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>

namespace JeremyAnsel.DirectX.Window.Wpf
{
    using System;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using System.Windows.Interop;
    using JeremyAnsel.DirectX.Window;

    public sealed class WindowHost : HwndHost
    {
        private readonly int width;

        private readonly int height;

        public WindowHost(WindowBase? window)
            : this(window, 0, 0)
        {
        }

        public WindowHost(WindowBase? window, double width, double height)
        {
            this.Window = window ?? throw new ArgumentNullException(nameof(window));

            if (width < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(width));
            }

            if (height < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(height));
            }

            this.width = (int)width;
            this.height = (int)height;
        }

        public WindowBase? Window { get; private set; }

        protected override HandleRef BuildWindowCore(HandleRef hwndParent)
        {
            this.Window!.BuildWindow(0, 0, this.width, this.height, hwndParent.Handle, true);

            Task.Factory.StartNew(() => this.Window.Run(), CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Current);

            return new HandleRef(this, this.Window.Handle);
        }

        protected override void DestroyWindowCore(HandleRef hwnd)
        {
            this.Window?.Destroy();
            this.Window = null;
        }

        protected override bool TabIntoCore(TraversalRequest request)
        {
            this.Window!.Focus();
            return true;
        }
    }
}

// <copyright file="WindowHost.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019, 2025 Jérémy Ansel
// </copyright>

namespace JeremyAnsel.DirectX.Window.Avalonia
{
    using global::Avalonia.Platform;
    using JeremyAnsel.DirectX.Window;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class WindowHost : global::Avalonia.Controls.NativeControlHost
    {
        public WindowHost(WindowBase? window)
        {
            this.Window = window ?? throw new ArgumentNullException(nameof(window));
        }

        public WindowBase? Window { get; private set; }

        protected override IPlatformHandle CreateNativeControlCore(IPlatformHandle hwndParent)
        {
            this.Window!.BuildWindow(0, 0, 0, 0, hwndParent.Handle, true);
            Task.Factory.StartNew(() => this.Window.Run(), CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Current);
            Thread.Sleep(100);
            return new PlatformHandle(this.Window!.Handle, "HWND");
        }

        protected override void DestroyNativeControlCore(IPlatformHandle control)
        {
            this.Window?.Destroy();
            this.Window = null;
            base.DestroyNativeControlCore(control);
        }
    }
}

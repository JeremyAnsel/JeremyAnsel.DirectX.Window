// <copyright file="WindowBase.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>

namespace JeremyAnsel.DirectX.Window
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    public abstract class WindowBase : INotifyPropertyChanged
    {
        private NativeWindow? window;

        private int width;

        private int height;

        private bool isActive;

        private bool isMinimized;

        private bool updateWhenInactive;

        private bool exitOnEscapeKey;

        protected WindowBase()
        {
            DpiHelpers.SetDpiAware();

            this.updateWhenInactive = true;
            this.exitOnEscapeKey = true;
        }

        protected virtual string DefaultTitle
        {
            get { return string.Format(CultureInfo.InvariantCulture, "{0}, {1} bits", AppDomain.CurrentDomain.FriendlyName, Environment.Is64BitProcess ? 64 : 32); }
        }

        public IntPtr Handle
        {
            get { return this.window is null ? IntPtr.Zero : this.window.Handle; }
        }

        public string? Title
        {
            get { return this.window?.Title; }

            set
            {
                if (this.window is not null)
                {
                    this.window.Title = value;
                }

                this.NotifyPropertyChanged();
            }
        }

        public int Width
        {
            get
            {
                return this.width;
            }

            internal set
            {
                if (this.width != value)
                {
                    this.width = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public int Height
        {
            get
            {
                return this.height;
            }

            internal set
            {
                if (this.height != value)
                {
                    this.height = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public bool IsActive
        {
            get
            {
                return this.isActive;
            }

            internal set
            {
                if (this.isActive != value)
                {
                    this.isActive = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public bool IsMinimized
        {
            get
            {
                return this.isMinimized;
            }

            internal set
            {
                if (this.isMinimized != value)
                {
                    this.isMinimized = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public WindowPerformanceTime? PerformanceTime
        {
            get { return this.window?.PerformanceTime; }
        }

        public bool UpdateWhenInactive
        {
            get
            {
                return this.updateWhenInactive;
            }

            set
            {
                if (this.updateWhenInactive != value)
                {
                    this.updateWhenInactive = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public bool ExitOnEscapeKey
        {
            get
            {
                return this.exitOnEscapeKey;
            }

            set
            {
                if (this.exitOnEscapeKey != value)
                {
                    this.exitOnEscapeKey = value;
                    this.NotifyPropertyChanged();
                }
            }
        }

        public bool IsChild { get; private set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed", Justification = "Reviewed")]
        protected void NotifyPropertyChanged([CallerMemberName] string? name = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void BuildWindow()
        {
            if (this.window != null)
            {
                throw new InvalidOperationException();
            }

            this.window = new NativeWindow(this, this.DefaultTitle);
            this.IsChild = false;
        }

        public void BuildWindow(int positionX, int positionY, int windowWidth, int windowHeight, IntPtr parentHandle, bool isChild)
        {
            if (this.window != null)
            {
                throw new InvalidOperationException();
            }

            this.window = new NativeWindow(this, this.DefaultTitle, positionX, positionY, windowWidth, windowHeight, parentHandle, isChild);
            this.IsChild = isChild;
        }

        public void Destroy()
        {
            this.window?.Destroy();
        }

        public void Exit()
        {
            this.window?.Exit();
        }

        [SuppressMessage("Design", "CA1031:Ne pas intercepter les types d'exception générale", Justification = "Reviewed.")]
        public void Run()
        {
            if (this.window == null)
            {
                this.BuildWindow();
            }

            if (this.window == null)
            {
                return;
            }

            try
            {
                this.window.Run();
                this.window.Destroy();
            }
            catch (Exception ex)
            {
                try
                {
                    string? title = this.window.Title;
                    this.window.Destroy();
                    MessageBox.Show(ex.ToString(), title, MessageBoxButton.Ok, MessageBoxIcon.Error);
                }
                catch
                {
                }
            }
        }

        protected internal abstract void Init();

        protected internal abstract void Update();

        protected internal abstract void Render();

        protected internal abstract void Present();

        protected internal abstract void OnWindowSizeChanged();

        protected internal virtual void OnEvent(WindowMessageType msg, IntPtr wParam, IntPtr lParam)
        {
            switch (msg)
            {
                case WindowMessageType.SetText:
                    this.NotifyPropertyChanged(nameof(Title));
                    break;
            }
        }

        protected internal virtual void OnKeyboardEvent(VirtualKey key, int repeatCount, bool wasDown, bool isDown)
        {
            if (this.exitOnEscapeKey)
            {
                if (isDown && key == VirtualKey.Escape)
                {
                    this.Exit();
                }
            }
        }

        public void Show(ShowWindow cmdShow)
        {
            NativeMethods.ShowWindow(this.Handle, cmdShow);
        }

        public void Focus()
        {
            NativeMethods.SetFocus(this.Handle);
        }
    }
}

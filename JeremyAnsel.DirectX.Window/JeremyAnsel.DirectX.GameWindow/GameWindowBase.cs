// <copyright file="GameWindowBase.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>

namespace JeremyAnsel.DirectX.GameWindow
{
    using System;
    using System.Text;
    using JeremyAnsel.DirectX.D3D11;
    using JeremyAnsel.DirectX.Window;
    using JeremyAnsel.DirectX.Dxgi;

    public abstract class GameWindowBase : WindowBase, IDeviceNotify
    {
        protected GameWindowBase()
        {
            this.RequestedD3DFeatureLevel = D3D11FeatureLevel.FeatureLevel91;
            this.DeviceResourcesOptions = new DeviceResourcesOptions();

            this.Timer = new StepTimer();
        }

        protected D3D11FeatureLevel RequestedD3DFeatureLevel { get; set; }

        protected DeviceResourcesOptions DeviceResourcesOptions { get; private set; }

        protected StepTimer Timer { get; private set; }

        protected SwapChainDeviceResources DeviceResources { get; private set; }

        protected FpsTextRenderer FpsTextRenderer { get; private set; }

        protected override string DefaultTitle
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                sb.Append(base.DefaultTitle);

                if (this.DeviceResources == null)
                {
                    sb.Append(", Initializing device...");
                }
                else
                {
                    sb.Append(", ");
                    sb.Append(this.DeviceResources.D3DDriverType);
                    sb.Append(", ");
                    sb.Append(this.DeviceResources.D3DFeatureLevel);

                    using (var device = new DxgiDevice2(this.DeviceResources.D3DDevice.Handle))
                    using (var adapter = device.GetAdapter())
                    {
                        sb.Append(", ");
                        sb.Append(adapter.Description.AdapterDescription);
                    }
                }

                return sb.ToString();
            }
        }

        protected T CheckMinimalFeatureLevel<T>(T component) where T : IGameComponent
        {
            if (component == null)
            {
                throw new ArgumentNullException(nameof(component));
            }

            D3D11FeatureLevel level = component.MinimalFeatureLevel;

            if (level > this.RequestedD3DFeatureLevel)
            {
                this.RequestedD3DFeatureLevel = level;
            }

            return component;
        }

        protected override void Init()
        {
            this.FpsTextRenderer = this.CheckMinimalFeatureLevel(new FpsTextRenderer(this.PerformanceTime));

            this.DeviceResources = new SwapChainDeviceResources(this, this.RequestedD3DFeatureLevel, this.DeviceResourcesOptions);
            this.DeviceResources.RegisterDeviceNotify(this);

            this.Title = this.DefaultTitle;

            this.CreateDeviceDependentResources();
        }

        protected override void OnWindowSizeChanged()
        {
            this.ReleaseWindowSizeDependentResources();
            this.DeviceResources.OnSizeChanged();
            this.CreateWindowSizeDependentResources();
        }

        public void OnDeviceLost()
        {
            this.ReleaseWindowSizeDependentResources();
            this.ReleaseDeviceDependentResources();
        }

        public void OnDeviceRestored()
        {
            this.CreateDeviceDependentResources();
            this.CreateWindowSizeDependentResources();
        }

        protected virtual void CreateDeviceDependentResources()
        {
            this.FpsTextRenderer.CreateDeviceDependentResources(this.DeviceResources);
        }

        protected virtual void ReleaseDeviceDependentResources()
        {
            this.FpsTextRenderer.ReleaseDeviceDependentResources();
        }

        protected virtual void CreateWindowSizeDependentResources()
        {
            this.FpsTextRenderer.CreateWindowSizeDependentResources();
        }

        protected virtual void ReleaseWindowSizeDependentResources()
        {
            this.FpsTextRenderer.ReleaseWindowSizeDependentResources();
        }

        protected override void Update()
        {
            this.Timer.Tick();

            this.FpsTextRenderer.Update(this.Timer);
        }

        protected override void Present()
        {
            this.FpsTextRenderer.Render();

            this.DeviceResources.Present();
        }

        protected override void OnEvent(WindowMessageType msg, IntPtr wParam, IntPtr lParam)
        {
            switch (msg)
            {
                case WindowMessageType.Destroy:
                    this.ReleaseWindowSizeDependentResources();
                    this.ReleaseDeviceDependentResources();

                    if (this.DeviceResources != null)
                    {
                        this.DeviceResources.Release();
                    }

                    break;
            }

            base.OnEvent(msg, wParam, lParam);
        }

        protected override void OnKeyboardEvent(VirtualKey key, int repeatCount, bool wasDown, bool isDown)
        {
            base.OnKeyboardEvent(key, repeatCount, wasDown, isDown);

            if (isDown && !wasDown)
            {
                switch (key)
                {
                    case VirtualKey.F12:
                        this.FpsTextRenderer.IsEnabled = !this.FpsTextRenderer.IsEnabled;
                        break;

                    case VirtualKey.F11:
                        if (!this.IsChild && this.DeviceResources.D3DDriverType == D3D11DriverType.Hardware)
                        {
                            bool fullscreen = this.DeviceResources.SwapChain.GetFullscreenState();
                            this.DeviceResources.SwapChain.SetFullscreenState(!fullscreen);
                        }

                        break;

                    case VirtualKey.F9:
                        this.DeviceResources.HandleDeviceLost();
                        break;
                }
            }
        }
    }
}

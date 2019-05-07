// <copyright file="GameWindowBase.cs" company="Jérémy Ansel">
// Copyright (c) 2015 Jérémy Ansel
// </copyright>

namespace JeremyAnsel.DirectX.GameWindow
{
    using System.Text;
    using JeremyAnsel.DirectX.D3D11;
    using JeremyAnsel.DirectX.Window;
    using JeremyAnsel.DirectX.Dxgi;

    public abstract class GameWindowBase : WindowBase, IDeviceNotify
    {
        protected GameWindowBase()
        {
            this.RequestedD3DFeatureLevel = D3D11FeatureLevel.FeatureLevel91;
            this.UseHighestD3DFeatureLevel = true;
            this.PreferMultisampling = false;

            this.Timer = new StepTimer();
        }

        protected D3D11FeatureLevel RequestedD3DFeatureLevel { get; set; }

        protected bool UseHighestD3DFeatureLevel { get; set; }

        protected bool PreferMultisampling { get; set; }

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

        protected override void Init()
        {
            this.FpsTextRenderer = new FpsTextRenderer(this.PerformanceTime);

            D3D11FeatureLevel minimalFeatureLevel = this.RequestedD3DFeatureLevel;

            if (this.FpsTextRenderer.MinimalFeatureLevel > minimalFeatureLevel)
            {
                minimalFeatureLevel = this.FpsTextRenderer.MinimalFeatureLevel;
            }

            this.DeviceResources = new SwapChainDeviceResources(this, minimalFeatureLevel, this.UseHighestD3DFeatureLevel, this.PreferMultisampling);
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
    }
}

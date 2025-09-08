// <copyright file="SwapChainDeviceResources.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>

namespace JeremyAnsel.DirectX.GameWindow
{
    using System;
    using JeremyAnsel.DirectX.D3D11;
    using JeremyAnsel.DirectX.Dxgi;
    using JeremyAnsel.DirectX.Window;

    public sealed class SwapChainDeviceResources : DeviceResources
    {
        private DxgiSwapChain2? swapChain;

        private readonly WindowBase window;

        private bool isFullscreen;

        public SwapChainDeviceResources(WindowBase? window)
            : this(window, D3D11FeatureLevel.FeatureLevel91, null)
        {
        }

        public SwapChainDeviceResources(WindowBase? window, D3D11FeatureLevel featureLevel, DeviceResourcesOptions? options)
            : base(featureLevel, options)
        {
            this.window = window ?? throw new ArgumentNullException(nameof(window));
        }

        public DxgiSwapChain2? SwapChain { get { return this.swapChain; } }

        protected override void OnReleaseBackBuffer()
        {
            if (!this.window.IsChild)
            {
                this.isFullscreen = this.swapChain ? this.swapChain!.GetFullscreenState() : false;

                if (this.isFullscreen)
                {
                    this.swapChain!.SetFullscreenState(false);
                }
            }

            DxgiUtils.DisposeAndNull(ref this.swapChain);
        }

        protected override D3D11Texture2D? OnCreateBackBuffer()
        {
            if (this.swapChain)
            {
                try
                {
                    this.swapChain!.ResizeBuffers(3, 0, 0, DxgiFormat.Unknown, DxgiSwapChainOptions.None);
                }
                catch (Exception ex)
                {
                    if (ex.HResult == DxgiError.DeviceRemoved || ex.HResult == DxgiError.DeviceReset)
                    {
                        this.HandleDeviceLost();
                        return null;
                    }

                    throw;
                }
            }
            else
            {
                //this.OnReleaseBackBuffer();

                DxgiSwapChainDesc1 swapChainDesc = new DxgiSwapChainDesc1
                {
                    Width = 0,
                    Height = 0,
                    Format = DxgiFormat.B8G8R8A8UNorm,
                    Stereo = false,
                    SampleDescription = new DxgiSampleDesc(1, 0),
                    BufferUsage = DxgiUsages.RenderTargetOutput | DxgiUsages.ShaderInput,
                    BufferCount = 3,
                    Scaling = DxgiScaling.None,
                    SwapEffect = DxgiSwapEffect.FlipSequential,
                    AlphaMode = DxgiAlphaMode.Ignore,
                    Options = DxgiSwapChainOptions.None
                };

                if (this.D3DFeatureLevel >= D3D11FeatureLevel.FeatureLevel110)
                {
                    swapChainDesc.BufferUsage |= DxgiUsages.UnorderedAcess;
                }

                //DxgiSwapChainFullscreenDesc? swapChainFullscreenDesc = null;

                /*if (this.D3DDriverType == D3D11DriverType.Hardware)
                {
                    swapChainFullscreenDesc = new DxgiSwapChainFullscreenDesc
                    {
                        IsWindowed = true,
                        RefreshRate = new DxgiRational(),
                        Scaling = DxgiModeScaling.Unspecified,
                        ScanlineOrdering = DxgiModeScanlineOrder.Unspecified
                    };
                }*/

                if (this.D3DDevice is null)
                {
                    return null;
                }

                using (var dxgiDevice = new DxgiDevice2(this.D3DDevice.Handle))
                using (var dxgiAdapter = dxgiDevice.GetAdapter())
                using (var dxgiFactory = dxgiAdapter.GetParent())
                {
                    try
                    {
                        this.swapChain = dxgiFactory.CreateSwapChainForWindowHandle(
                            this.D3DDevice.Handle,
                            this.window.Handle,
                            swapChainDesc,
                            null,
                            null);
                    }
                    catch (Exception ex)
                    {
                        if (ex.HResult == DxgiError.InvalidCall)
                        {
                            swapChainDesc.SwapEffect = DxgiSwapEffect.Sequential;

                            this.swapChain = dxgiFactory.CreateSwapChainForWindowHandle(
                                this.D3DDevice.Handle,
                                this.window.Handle,
                                swapChainDesc,
                                null,
                                null);
                        }
                        else
                        {
                            throw;
                        }
                    }

                    dxgiDevice.MaximumFrameLatency = 1;
                }

                if (!this.window.IsChild)
                {
                    if (this.isFullscreen)
                    {
                        this.swapChain.SetFullscreenState(true);
                    }
                }
            }

            return this.swapChain.GetTexture2D(0);
        }

        protected override void OnPresent()
        {
            this.swapChain?.Present(1, DxgiPresentOptions.None);
        }
    }
}

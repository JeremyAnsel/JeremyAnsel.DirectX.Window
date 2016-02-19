// <copyright file="SwapChainDeviceResources.cs" company="Jérémy Ansel">
// Copyright (c) 2015 Jérémy Ansel
// </copyright>

namespace JeremyAnsel.DirectX.GameWindow
{
    using System;
    using JeremyAnsel.DirectX.D3D11;
    using JeremyAnsel.DirectX.Dxgi;
    using JeremyAnsel.DirectX.Window;

    public sealed class SwapChainDeviceResources : DeviceResources
    {
        private DxgiSwapChain2 swapChain;

        private WindowBase window;

        public SwapChainDeviceResources(WindowBase window)
            : this(window, D3D11FeatureLevel.FeatureLevel91, true, false)
        {
        }

        public SwapChainDeviceResources(WindowBase window, D3D11FeatureLevel featureLevel, bool useHighestFeatureLevel, bool preferMultisampling)
            : base(featureLevel, useHighestFeatureLevel, preferMultisampling)
        {
            if (window == null)
            {
                throw new ArgumentNullException("window");
            }

            this.window = window;
        }

        public DxgiSwapChain2 SwapChain { get { return this.swapChain; } }

        protected override void OnReleaseBackBuffer()
        {
            DxgiUtils.DisposeAndNull(ref this.swapChain);
        }

        protected override D3D11Texture2D OnCreateBackBuffer()
        {
            if (this.swapChain)
            {
                try
                {
                    this.swapChain.ResizeBuffers(3, 0, 0, DxgiFormat.Unknown, DxgiSwapChainOptions.None);
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
                DxgiSwapChainDesc1 swapChainDesc = new DxgiSwapChainDesc1
                {
                    Width = 0,
                    Height = 0,
                    Format = DxgiFormat.B8G8R8A8UNorm,
                    Stereo = false,
                    SampleDescription = new DxgiSampleDesc(1, 0),
                    BufferUsage = DxgiUsages.RenderTargetOutput,
                    BufferCount = 3,
                    Scaling = DxgiScaling.None,
                    SwapEffect = DxgiSwapEffect.FlipSequential,
                    AlphaMode = DxgiAlphaMode.Ignore,
                    Options = DxgiSwapChainOptions.None
                };

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
            }

            return this.swapChain.GetTexture2D(0);
        }

        protected override void OnPresent()
        {
            this.swapChain.Present(1, DxgiPresentOptions.None);
        }
    }
}

// <copyright file="DeviceResources.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>

namespace JeremyAnsel.DirectX.GameWindow
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Linq;
    using System.Runtime.InteropServices;
    using JeremyAnsel.DirectX.D2D1;
    using JeremyAnsel.DirectX.D3D11;
    using JeremyAnsel.DirectX.DWrite;
    using JeremyAnsel.DirectX.Dxgi;

    public abstract class DeviceResources
    {
        private readonly DeviceResourcesOptions options;

        private D3D11Device d3dDevice;

        private D3D11DeviceContext d3dContext;

        private D3D11Texture2D backBuffer;

        private D3D11Texture2D offscreenBuffer;

        private uint backBufferWidth;

        private uint backBufferHeight;

        private D3D11RenderTargetView d3dRenderTargetView;

        private D3D11DepthStencilView d3dDepthStencilView;

        private D3D11Viewport screenViewport;

        private D2D1Factory d2dFactory;

        private D2D1RenderTarget d2dRenderTarget;

        private DWriteFactory dwriteFactory;

        private D3D11DriverType d3dDriverType;

        private D3D11FeatureLevel d3dFeatureLevel;

        private DxgiSampleDesc d3dSampleDesc;

        private IDeviceNotify deviceNotify;

        private float dpiX;

        private float dpiY;

        protected DeviceResources(D3D11FeatureLevel featureLevel, DeviceResourcesOptions options)
        {
            this.d3dFeatureLevel = featureLevel;
            this.options = options ?? new DeviceResourcesOptions();

            this.CreateDeviceIndependentResources();
            this.CreateDeviceResources();
        }

        public D3D11Device D3DDevice { get { return this.d3dDevice; } }

        public D3D11DeviceContext D3DContext { get { return this.d3dContext; } }

        public D3D11Texture2D BackBuffer { get { return this.backBuffer; } }

        public uint BackBufferWidth { get { return this.backBufferWidth; } }

        public uint BackBufferHeight { get { return this.backBufferHeight; } }

        public D3D11RenderTargetView D3DRenderTargetView { get { return this.d3dRenderTargetView; } }

        public D3D11DepthStencilView D3DDepthStencilView { get { return this.d3dDepthStencilView; } }

        public D3D11Viewport ScreenViewport { get { return this.screenViewport; } }

        public D2D1Factory D2DFactory { get { return this.d2dFactory; } }

        public D2D1RenderTarget D2DRenderTarget { get { return this.d2dRenderTarget; } }

        public DWriteFactory DWriteFactory { get { return this.dwriteFactory; } }

        public D3D11DriverType D3DDriverType { get { return this.d3dDriverType; } }

        public D3D11FeatureLevel D3DFeatureLevel { get { return this.d3dFeatureLevel; } }

        public DxgiSampleDesc D3DSampleDesc { get { return this.d3dSampleDesc; } }

        public float DpiX { get { return this.dpiX; } }

        public float DpiY { get { return this.dpiY; } }

        public DxgiAdapterDesc2 AdapterDescription
        {
            get
            {
                using (var dxgiDevice = new DxgiDevice2(this.d3dDevice.Handle))
                using (var dxgiAdapter = dxgiDevice.GetAdapter())
                {
                    return dxgiAdapter.Description;
                }
            }
        }

        public float ConvertPixelsToDipsX(float pixels)
        {
            return (float)Math.Floor(pixels * 96.0f / this.dpiX + 0.5f);
        }

        public float ConvertPixelsToDipsY(float pixels)
        {
            return (float)Math.Floor(pixels * 96.0f / this.dpiY + 0.5f);
        }

        public float ConvertDipsToPixelsX(float dips)
        {
            return (float)Math.Floor(dips * this.dpiX / 96.0f + 0.5f);
        }

        public float ConvertDipsToPixelsY(float dips)
        {
            return (float)Math.Floor(dips * this.dpiY / 96.0f + 0.5f);
        }

        public void RegisterDeviceNotify(IDeviceNotify notify)
        {
            this.deviceNotify = notify;
        }

        public void ValidateDevice()
        {
            DxgiAdapterDesc2 previousDesc;

            using (var dxgiDevice = new DxgiDevice2(this.d3dDevice.Handle))
            using (var deviceAdapter = dxgiDevice.GetAdapter())
            using (var deviceFactory = deviceAdapter.GetParent())
            using (var previousDefaultAdapter = deviceFactory.EnumAdapters().First())
            {
                previousDesc = previousDefaultAdapter.Description;
            }

            DxgiAdapterDesc2 currentDesc;

            using (var currentFactory = DxgiFactory2.Create())
            using (var currentDefaultAdapter = currentFactory.EnumAdapters().First())
            {
                currentDesc = currentDefaultAdapter.Description;
            }

            if (previousDesc.AdapterLuid != currentDesc.AdapterLuid || this.d3dDevice.GetDeviceRemovedReason() != null)
            {
                this.HandleDeviceLost();
            }
        }

        public void Release()
        {
            this.ReleaseDeviceResources();
            this.ReleaseWindowSizeDependentResources();
            this.OnReleaseBackBuffer();
        }

        public void HandleDeviceLost()
        {
            if (this.deviceNotify != null)
            {
                this.deviceNotify.OnDeviceLost();
            }

            this.ReleaseDeviceResources();
            this.ReleaseWindowSizeDependentResources();
            this.OnReleaseBackBuffer();

            this.CreateDeviceResources();
            this.CreateWindowSizeDependentResources();

            if (this.deviceNotify != null)
            {
                this.deviceNotify.OnDeviceRestored();
            }
        }

        public void OnSizeChanged()
        {
            this.ReleaseWindowSizeDependentResources();
            this.CreateWindowSizeDependentResources();
        }

        public void Present()
        {
            try
            {
                if (this.d3dSampleDesc.Count > 1)
                {
                    this.d3dContext.ResolveSubresource(this.backBuffer, 0, this.offscreenBuffer, 0, DxgiFormat.B8G8R8A8UNorm);
                }

                this.OnPresent();
            }
            catch (Exception ex)
            {
                if (ex.HResult == DxgiError.DeviceRemoved || ex.HResult == DxgiError.DeviceReset)
                {
                    this.HandleDeviceLost();
                    return;
                }

                throw;
            }
        }

        public void SaveBackBuffer(string fileName)
        {
            ImageFormat format;

            switch (System.IO.Path.GetExtension(fileName).ToUpperInvariant())
            {
                case ".BMP":
                    format = ImageFormat.Bmp;
                    break;

                case ".PNG":
                    format = ImageFormat.Png;
                    break;

                case ".JPG":
                    format = ImageFormat.Jpeg;
                    break;

                default:
                    throw new InvalidOperationException();
            }

            var textureDescription = this.backBuffer.Description;
            textureDescription.BindOptions = D3D11BindOptions.None;
            textureDescription.CpuAccessOptions = D3D11CpuAccessOptions.Read | D3D11CpuAccessOptions.Write;
            textureDescription.Usage = D3D11Usage.Staging;

            using (var texture = this.d3dDevice.CreateTexture2D(textureDescription))
            {
                this.d3dContext.CopyResource(texture, this.backBuffer);

                var map = this.d3dContext.Map(texture, 0, D3D11MapCpuPermission.Read, D3D11MapOptions.None);

                try
                {
                    using (var bitmap = new Bitmap((int)textureDescription.Width, (int)textureDescription.Height, (int)map.RowPitch, PixelFormat.Format32bppArgb, map.Data))
                    {
                        if (format == ImageFormat.Jpeg)
                        {
                            ImageCodecInfo encoder = ImageCodecInfo
                                .GetImageEncoders()
                                .First(c => c.FormatID == ImageFormat.Jpeg.Guid);

                            var encParams = new EncoderParameters(1);
                            encParams.Param[0] = new EncoderParameter(Encoder.Quality, 100L);

                            bitmap.Save(fileName, encoder, encParams);
                        }
                        else
                        {
                            bitmap.Save(fileName, format);
                        }
                    }
                }
                finally
                {
                    this.d3dContext.Unmap(texture, 0);
                }
            }
        }

        public byte[] GetBackBufferContent()
        {
            var textureDescription = this.backBuffer.Description;
            textureDescription.BindOptions = D3D11BindOptions.None;
            textureDescription.CpuAccessOptions = D3D11CpuAccessOptions.Read | D3D11CpuAccessOptions.Write;
            textureDescription.Usage = D3D11Usage.Staging;

            using (var texture = this.d3dDevice.CreateTexture2D(textureDescription))
            {
                this.d3dContext.CopyResource(texture, this.backBuffer);

                var map = this.d3dContext.Map(texture, 0, D3D11MapCpuPermission.Read, D3D11MapOptions.None);

                try
                {
                    byte[] buffer = new byte[textureDescription.Width * textureDescription.Height * 4];

                    for (int line = 0; line < textureDescription.Height; line++)
                    {
                        Marshal.Copy(IntPtr.Add(map.Data, line * (int)map.RowPitch), buffer, line * (int)textureDescription.Width * 4, (int)textureDescription.Width * 4);
                    }

                    return buffer;
                }
                finally
                {
                    this.d3dContext.Unmap(texture, 0);
                }
            }
        }

        protected abstract void OnReleaseBackBuffer();

        protected abstract D3D11Texture2D OnCreateBackBuffer();

        protected abstract void OnPresent();

        private DxgiSampleDesc CheckMultisamplingSupport()
        {
            DxgiSampleDesc sampleDesc = new DxgiSampleDesc(1, 0);

            if (this.d3dDevice.CheckFormatSupport(DxgiFormat.B8G8R8A8UNorm, out D3D11FormatSupport formatSupport))
            {
                return sampleDesc;
            }

            if (!formatSupport.HasFlag(D3D11FormatSupport.MultisampleResolve | D3D11FormatSupport.MultisampleRenderTarget))
            {
                return sampleDesc;
            }

            for (uint i = 2; i <= D3D11Constants.MaxMultisampleSampleCount; i *= 2)
            {
                if (!this.d3dDevice.CheckMultisampleQualityLevels(DxgiFormat.B8G8R8A8UNorm, i, out uint numQualityLevels))
                {
                    if (numQualityLevels > 0)
                    {
                        sampleDesc.Count = i;
                        sampleDesc.Quality = numQualityLevels - 1;
                    }
                }
            }

            return sampleDesc;
        }

        private void CreateDeviceIndependentResources()
        {
            if (this.options.Debug)
            {
                this.d2dFactory = D2D1Factory.Create(D2D1FactoryType.SingleThreaded, D2D1DebugLevel.Warning);
            }
            else
            {
                this.d2dFactory = D2D1Factory.Create(D2D1FactoryType.SingleThreaded);
            }

            this.dwriteFactory = DWriteFactory.Create(DWriteFactoryType.Shared);
        }

        private void CreateDeviceResources()
        {
            D3D11FeatureLevel[] featureLevels;

            if (this.options.UseHighestFeatureLevel)
            {
                if (this.d3dFeatureLevel == D3D11FeatureLevel.FeatureLevel91)
                {
                    featureLevels = new D3D11FeatureLevel[]
                    {
                        D3D11FeatureLevel.FeatureLevel111,
                        D3D11FeatureLevel.FeatureLevel110,
                        D3D11FeatureLevel.FeatureLevel101,
                        D3D11FeatureLevel.FeatureLevel100,
                        D3D11FeatureLevel.FeatureLevel93,
                        D3D11FeatureLevel.FeatureLevel92,
                        D3D11FeatureLevel.FeatureLevel91
                    };
                }
                else
                {
                    featureLevels = Enum.GetValues(typeof(D3D11FeatureLevel))
                        .Cast<D3D11FeatureLevel>()
                        .Where(t => t >= this.d3dFeatureLevel)
                        .OrderByDescending(t => t)
                        .ToArray();
                }
            }
            else
            {
                featureLevels = new D3D11FeatureLevel[]
                {
                    this.d3dFeatureLevel
                };
            }

            D3D11CreateDeviceOptions createDeviceOptions = D3D11CreateDeviceOptions.BgraSupport;

            if (this.options.Debug)
            {
                createDeviceOptions |= D3D11CreateDeviceOptions.Debug;
            }

            if (this.options.ForceWarp)
            {
                this.d3dDriverType = D3D11DriverType.Warp;

                D3D11Device.CreateDevice(
                    null,
                    this.d3dDriverType,
                    createDeviceOptions,
                    featureLevels,
                    out this.d3dDevice,
                    out this.d3dFeatureLevel,
                    out this.d3dContext);
            }
            else
            {
                try
                {
                    this.d3dDriverType = D3D11DriverType.Hardware;

                    D3D11Device.CreateDevice(
                        null,
                        this.d3dDriverType,
                        createDeviceOptions,
                        featureLevels,
                        out this.d3dDevice,
                        out this.d3dFeatureLevel,
                        out this.d3dContext);
                }
                catch (Exception ex)
                {
                    if (ex.HResult == DxgiError.Unsupported)
                    {
                        this.d3dDriverType = D3D11DriverType.Warp;

                        D3D11Device.CreateDevice(
                            null,
                            this.d3dDriverType,
                            createDeviceOptions,
                            featureLevels,
                            out this.d3dDevice,
                            out this.d3dFeatureLevel,
                            out this.d3dContext);
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            if (this.options.PreferMultisampling)
            {
                this.d3dSampleDesc = this.CheckMultisamplingSupport();
            }
            else
            {
                this.d3dSampleDesc = new DxgiSampleDesc(1, 0);
            }
        }

        private void ReleaseDeviceResources()
        {
            D3D11Utils.DisposeAndNull(ref this.d3dContext);
            D3D11Utils.DisposeAndNull(ref this.d3dDevice);
        }

        private void CreateWindowSizeDependentResources()
        {
            //this.ReleaseWindowSizeDependentResources();
            this.d3dContext.Flush();

            var createdBackBuffer = this.OnCreateBackBuffer();

            if (createdBackBuffer == null)
            {
                return;
            }

            this.backBuffer = createdBackBuffer;

            var backBufferDesc = this.backBuffer.Description;
            this.backBufferWidth = backBufferDesc.Width;
            this.backBufferHeight = backBufferDesc.Height;

            if (this.d3dSampleDesc.Count > 1)
            {
                D3D11Texture2DDesc desc = new D3D11Texture2DDesc(
                    DxgiFormat.B8G8R8A8UNorm,
                    this.backBufferWidth,
                    this.backBufferHeight,
                    1,
                    1,
                    D3D11BindOptions.RenderTarget,
                    D3D11Usage.Default,
                    D3D11CpuAccessOptions.None,
                    this.d3dSampleDesc.Count,
                    this.d3dSampleDesc.Quality,
                    D3D11ResourceMiscOptions.None);

                this.offscreenBuffer = this.D3DDevice.CreateTexture2D(desc);
            }

            if (this.d3dSampleDesc.Count > 1)
            {
                D3D11RenderTargetViewDesc renderTargetViewDesc = new D3D11RenderTargetViewDesc(D3D11RtvDimension.Texture2DMs);

                this.d3dRenderTargetView = this.d3dDevice.CreateRenderTargetView(this.offscreenBuffer, renderTargetViewDesc);
            }
            else
            {
                D3D11RenderTargetViewDesc renderTargetViewDesc = new D3D11RenderTargetViewDesc(D3D11RtvDimension.Texture2D);

                this.d3dRenderTargetView = this.d3dDevice.CreateRenderTargetView(this.backBuffer, renderTargetViewDesc);
            }

            D3D11Texture2DDesc depthStencilDesc = new D3D11Texture2DDesc
            {
                Width = this.backBufferWidth,
                Height = this.backBufferHeight,
                MipLevels = 1,
                ArraySize = 1,
                Format = DxgiFormat.D24UNormS8UInt,
                SampleDesc = this.d3dSampleDesc,
                Usage = D3D11Usage.Default,
                BindOptions = D3D11BindOptions.DepthStencil,
                CpuAccessOptions = D3D11CpuAccessOptions.None,
                MiscOptions = D3D11ResourceMiscOptions.None
            };

            using (var depthStencil = this.d3dDevice.CreateTexture2D(depthStencilDesc))
            {
                D3D11DepthStencilViewDesc depthStencilViewDesc = new D3D11DepthStencilViewDesc(this.d3dSampleDesc.Count > 1 ? D3D11DsvDimension.Texture2DMs : D3D11DsvDimension.Texture2D);

                this.d3dDepthStencilView = this.d3dDevice.CreateDepthStencilView(depthStencil, depthStencilViewDesc);
            }

            this.screenViewport = new D3D11Viewport
            {
                TopLeftX = 0,
                TopLeftY = 0,
                Width = this.backBufferWidth,
                Height = this.backBufferHeight,
                MinDepth = 0.0f,
                MaxDepth = 1.0f
            };

            this.d3dContext.RasterizerStageSetViewports(new[] { this.screenViewport });

            using (var surface = new DxgiSurface2(this.d3dSampleDesc.Count > 1 ? this.offscreenBuffer.Handle : this.backBuffer.Handle))
            {
                this.d2dFactory.GetDesktopDpi(out this.dpiX, out this.dpiY);

                var properties = new D2D1RenderTargetProperties(
                    D2D1RenderTargetType.Default,
                    new D2D1PixelFormat(DxgiFormat.B8G8R8A8UNorm, D2D1AlphaMode.Premultiplied),
                    dpiX,
                    dpiY,
                    D2D1RenderTargetUsages.None,
                    D2D1FeatureLevel.Default);

                this.d2dRenderTarget = this.d2dFactory.CreateDxgiSurfaceRenderTarget(surface, properties);
            }

            this.d2dRenderTarget.AntialiasMode = D2D1AntialiasMode.PerPrimitive;
            this.d2dRenderTarget.TextAntialiasMode = D2D1TextAntialiasMode.Grayscale;

            D3D11RasterizerDesc rasterizerStateDesc = new D3D11RasterizerDesc(D3D11FillMode.Solid, D3D11CullMode.Back, false, 0, 0.0f, 0.0f, true, false, true, false);

            using (var rasterizerState = this.d3dDevice.CreateRasterizerState(rasterizerStateDesc))
            {
                this.d3dContext.RasterizerStageSetState(rasterizerState);
            }
        }

        private void ReleaseWindowSizeDependentResources()
        {
            if (this.d3dContext)
            {
                this.d3dContext.OutputMergerSetRenderTargets(new D3D11RenderTargetView[] { null }, null);
                this.d3dContext.ClearState();
            }

            D3D11Utils.DisposeAndNull(ref this.backBuffer);
            D3D11Utils.DisposeAndNull(ref this.offscreenBuffer);
            D3D11Utils.DisposeAndNull(ref this.d3dRenderTargetView);
            D3D11Utils.DisposeAndNull(ref this.d3dDepthStencilView);
            D2D1Utils.DisposeAndNull(ref this.d2dRenderTarget);
        }
    }
}

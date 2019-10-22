// <copyright file="RenderTargetDeviceResources.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>

namespace JeremyAnsel.DirectX.GameWindow
{
    using JeremyAnsel.DirectX.D3D11;
    using JeremyAnsel.DirectX.Dxgi;
    using System;

    public sealed class RenderTargetDeviceResources : DeviceResources
    {
        private uint width;

        private uint height;

        public RenderTargetDeviceResources(uint width, uint height)
            : this(width, height, D3D11FeatureLevel.FeatureLevel91, null)
        {
        }

        public RenderTargetDeviceResources(uint width, uint height, D3D11FeatureLevel featureLevel, DeviceResourcesOptions options)
            : base(featureLevel, options)
        {
            if (width < 0)
            {
                throw new ArgumentOutOfRangeException("width");
            }

            if (height < 0)
            {
                throw new ArgumentOutOfRangeException("height");
            }

            this.width = width;
            this.height = height;

            this.OnSizeChanged();
        }

        protected override void OnReleaseBackBuffer()
        {
        }

        protected override D3D11Texture2D OnCreateBackBuffer()
        {
            D3D11Texture2DDesc backBufferDesc = new D3D11Texture2DDesc(
                DxgiFormat.B8G8R8A8UNorm,
                this.width,
                this.height,
                1,
                1,
                D3D11BindOptions.RenderTarget,
                D3D11Usage.Default,
                D3D11CpuAccessOptions.None,
                1,
                0,
                D3D11ResourceMiscOptions.None);

            return this.D3DDevice.CreateTexture2D(backBufferDesc);
        }

        protected override void OnPresent()
        {
            this.D3DDevice.ThrowDeviceRemovedReason();
        }
    }
}

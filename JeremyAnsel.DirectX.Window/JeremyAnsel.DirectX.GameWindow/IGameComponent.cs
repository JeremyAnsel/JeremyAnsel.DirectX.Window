// <copyright file="IGameComponent.cs" company="Jérémy Ansel">
// Copyright (c) 2015-2026 Jérémy Ansel
// </copyright>

using JeremyAnsel.DirectX.D3D11;

namespace JeremyAnsel.DirectX.GameWindow
{
    public interface IGameComponent
    {
        D3D11FeatureLevel MinimalFeatureLevel { get; }

        void CreateDeviceDependentResources(DeviceResources resources);

        void ReleaseDeviceDependentResources();

        void CreateWindowSizeDependentResources();

        void ReleaseWindowSizeDependentResources();

        void Update(ITimer timer);

        void Render();
    }
}

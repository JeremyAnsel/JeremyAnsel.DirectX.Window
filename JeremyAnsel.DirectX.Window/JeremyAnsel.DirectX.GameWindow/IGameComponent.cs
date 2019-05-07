// <copyright file="IGameComponent.cs" company="Jérémy Ansel">
// Copyright (c) 2015 Jérémy Ansel
// </copyright>

namespace JeremyAnsel.DirectX.GameWindow
{
    using JeremyAnsel.DirectX.D3D11;

    public interface IGameComponent
    {
        D3D11FeatureLevel MinimalFeatureLevel { get; }

        void CreateDeviceDependentResources(DeviceResources resources);

        void ReleaseDeviceDependentResources();

        void CreateWindowSizeDependentResources();

        void Update(StepTimer timer);

        void Render();
    }
}

// <copyright file="IDeviceNotify.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>

namespace JeremyAnsel.DirectX.GameWindow
{
    public interface IDeviceNotify
    {
        void OnDeviceLost();

        void OnDeviceRestored();
    }
}

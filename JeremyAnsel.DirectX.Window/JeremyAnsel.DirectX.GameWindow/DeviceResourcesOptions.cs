// <copyright file="DeviceResourcesOptions.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>

namespace JeremyAnsel.DirectX.GameWindow
{
    public sealed class DeviceResourcesOptions
    {
        public bool Debug { get; set; } = false;

        public bool ForceWarp { get; set; } = false;

        public bool PreferMultisampling { get; set; } = false;

        public bool UseHighestFeatureLevel { get; set; } = true;
    }
}

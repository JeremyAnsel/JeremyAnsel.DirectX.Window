// <copyright file="ITimer.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>

namespace JeremyAnsel.DirectX.GameWindow
{
    using System;

    public interface ITimer
    {
        uint FrameCount { get; }

        uint FramesPerSecond { get; }

        double TotalSeconds { get; }

        double ElapsedSeconds { get; }

        TimeSpan TotalTime { get; }
    }
}

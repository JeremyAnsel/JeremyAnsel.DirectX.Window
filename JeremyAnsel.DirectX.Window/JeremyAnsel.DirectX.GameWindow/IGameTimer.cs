// <copyright file="IGameTimer.cs" company="Jérémy Ansel">
// Copyright (c) 2015-2026 Jérémy Ansel
// </copyright>

namespace JeremyAnsel.DirectX.GameWindow
{
    public interface IGameTimer
    {
        uint FrameCount { get; }

        uint FramesPerSecond { get; }

        double TotalSeconds { get; }

        double ElapsedSeconds { get; }

        TimeSpan TotalTime { get; }
    }
}

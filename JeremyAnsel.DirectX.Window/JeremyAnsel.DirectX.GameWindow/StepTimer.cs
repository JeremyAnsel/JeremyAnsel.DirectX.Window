// <copyright file="StepTimer.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>

namespace JeremyAnsel.DirectX.GameWindow
{
    using System;
    using System.Diagnostics;

    public sealed class StepTimer : ITimer
    {
        private const double MaxDelta = .25;

        private readonly Stopwatch stopwatch;

        private double lastSeconds;

        private uint framesThisSecond;

        private double secondCounter;

        public StepTimer()
        {
            this.stopwatch = Stopwatch.StartNew();
        }

        public uint FrameCount { get; private set; }

        public uint FramesPerSecond { get; private set; }

        public double TotalSeconds { get; private set; }

        public double ElapsedSeconds { get; private set; }

        public TimeSpan TotalTime
        {
            get { return new TimeSpan(0, 0, (int)this.TotalSeconds); }
        }

        public void Reset()
        {
            this.FrameCount = 0;
            this.lastSeconds = 0;
            this.TotalSeconds = 0;
            this.ElapsedSeconds = 0;
            this.FramesPerSecond = 0;
            this.framesThisSecond = 0;
            this.secondCounter = 0;
            this.stopwatch.Restart();
        }

        public void Tick()
        {
            double currentSeconds = this.stopwatch.Elapsed.TotalSeconds;

            this.ElapsedSeconds = currentSeconds - this.lastSeconds;

            this.secondCounter += this.ElapsedSeconds;

            if (this.ElapsedSeconds > MaxDelta)
            {
                this.ElapsedSeconds = MaxDelta;
            }

            this.TotalSeconds += this.ElapsedSeconds;

            this.FrameCount++;

            this.lastSeconds = currentSeconds;

            this.framesThisSecond++;

            if (this.secondCounter > 1)
            {
                this.FramesPerSecond = this.framesThisSecond;
                this.framesThisSecond = 0;
                this.secondCounter %= 1;
            }
        }
    }
}

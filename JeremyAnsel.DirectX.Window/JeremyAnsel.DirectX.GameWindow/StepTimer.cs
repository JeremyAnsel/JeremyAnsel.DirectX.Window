// <copyright file="StepTimer.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>

namespace JeremyAnsel.DirectX.GameWindow
{
    using System;
    using System.Diagnostics;

    public sealed class StepTimer
    {
        private const double MaxDelta = .25;

        private readonly Stopwatch stopwatch;

        private uint frameCount;

        private double lastSeconds;

        private double totalSeconds;

        private double elapsedSeconds;

        private uint framesPerSecond;

        private uint framesThisSecond;

        private double secondCounter;

        public StepTimer()
        {
            this.stopwatch = Stopwatch.StartNew();
        }

        public uint FrameCount
        {
            get { return this.frameCount; }
        }

        public uint FramesPerSecond
        {
            get { return this.framesPerSecond; }
        }

        public double TotalSeconds
        {
            get { return this.totalSeconds; }
        }

        public double ElapsedSeconds
        {
            get { return this.elapsedSeconds; }
        }

        public TimeSpan TotalTime
        {
            get { return new TimeSpan(0, 0, (int)this.totalSeconds); }
        }

        public void Reset()
        {
            this.frameCount = 0;
            this.lastSeconds = 0;
            this.totalSeconds = 0;
            this.elapsedSeconds = 0;
            this.framesPerSecond = 0;
            this.framesThisSecond = 0;
            this.secondCounter = 0;
            this.stopwatch.Restart();
        }

        public void Tick()
        {
            double currentSeconds = this.stopwatch.Elapsed.TotalSeconds;

            this.elapsedSeconds = currentSeconds - this.lastSeconds;

            this.secondCounter += this.elapsedSeconds;

            if (this.elapsedSeconds > StepTimer.MaxDelta)
            {
                this.elapsedSeconds = StepTimer.MaxDelta;
            }

            this.totalSeconds += this.elapsedSeconds;

            this.frameCount++;

            this.lastSeconds = currentSeconds;

            this.framesThisSecond++;

            if (this.secondCounter > 1)
            {
                this.framesPerSecond = this.framesThisSecond;
                this.framesThisSecond = 0;
                this.secondCounter %= 1;
            }
        }
    }
}

// <copyright file="FixedTimer.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>

namespace JeremyAnsel.DirectX.GameWindow
{
    using System;

    public sealed class FixedTimer : ITimer
    {
        private const double MaxDelta = .25;

        private const double MinDelta = .001;

        public FixedTimer()
        {
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
            this.Reset(0);
        }

        public void Reset(double totalSeconds)
        {
            if (totalSeconds < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(totalSeconds));
            }

            this.FrameCount = 0;
            this.TotalSeconds = totalSeconds;
            this.ElapsedSeconds = 0;
            this.FramesPerSecond = 0;
        }

        public void Tick(double elapsedSeconds)
        {
            if (elapsedSeconds < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(elapsedSeconds));
            }

            this.ElapsedSeconds = elapsedSeconds;

            if (this.ElapsedSeconds > MaxDelta)
            {
                this.ElapsedSeconds = MaxDelta;
            }
            else if (this.ElapsedSeconds < MinDelta)
            {
                this.ElapsedSeconds = MinDelta;
            }

            this.TotalSeconds += this.ElapsedSeconds;

            this.FrameCount++;

            if (this.ElapsedSeconds == 0)
            {
                this.FramesPerSecond = 0;
            }
            else
            {
                this.FramesPerSecond = (uint)(1.0 / this.ElapsedSeconds);
            }
        }
    }
}

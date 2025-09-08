// <copyright file="WindowPerformanceTime.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>

namespace JeremyAnsel.DirectX.Window
{
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.CompilerServices;

    public sealed class WindowPerformanceTime : INotifyPropertyChanged
    {
        private readonly Stopwatch stopwatch = new Stopwatch();

        private double elapsedEventTime = 0;

        private double elapsedUpdateTime = 0;

        private double elapsedRenderTime = 0;

        private double elapsedPresentTime = 0;

        private int eventTime;

        private int updateTime;

        private int renderTime;

        private int presentTime;

        internal WindowPerformanceTime()
        {
        }

        public int EventTime
        {
            get
            {
                return this.eventTime;
            }

            private set
            {
                this.eventTime = value;
                this.NotifyPropertyChanged();
            }
        }

        public int UpdateTime
        {
            get
            {
                return this.updateTime;
            }

            private set
            {
                this.updateTime = value;
                this.NotifyPropertyChanged();
            }
        }

        public int RenderTime
        {
            get
            {
                return this.renderTime;
            }

            private set
            {
                this.renderTime = value;
                this.NotifyPropertyChanged();
            }
        }

        public int PresentTime
        {
            get
            {
                return this.presentTime;
            }

            private set
            {
                this.presentTime = value;
                this.NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed", Justification = "Reviewed")]
        private void NotifyPropertyChanged([CallerMemberName] string? name = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        internal void Tick()
        {
            double elapsedTime = this.elapsedEventTime + this.elapsedUpdateTime + this.elapsedRenderTime + this.elapsedPresentTime;

            if (elapsedTime > 1000)
            {
                this.EventTime = (int)(this.elapsedEventTime * 1000 / elapsedTime);
                this.UpdateTime = (int)(this.elapsedUpdateTime * 1000 / elapsedTime);
                this.RenderTime = (int)(this.elapsedRenderTime * 1000 / elapsedTime);
                this.PresentTime = (int)(this.elapsedPresentTime * 1000 / elapsedTime);

                this.elapsedEventTime = 0;
                this.elapsedUpdateTime = 0;
                this.elapsedRenderTime = 0;
                this.elapsedPresentTime = 0;
            }
        }

        internal void Start()
        {
            this.stopwatch.Restart();
        }

        internal void StopEvent()
        {
            this.stopwatch.Stop();
            this.elapsedEventTime += this.stopwatch.Elapsed.TotalMilliseconds;
        }

        internal void StopUpdate()
        {
            this.stopwatch.Stop();
            this.elapsedUpdateTime += this.stopwatch.Elapsed.TotalMilliseconds;
        }

        internal void StopRender()
        {
            this.stopwatch.Stop();
            this.elapsedRenderTime += this.stopwatch.Elapsed.TotalMilliseconds;
        }

        internal void StopPresent()
        {
            this.stopwatch.Stop();
            this.elapsedPresentTime += this.stopwatch.Elapsed.TotalMilliseconds;
        }
    }
}

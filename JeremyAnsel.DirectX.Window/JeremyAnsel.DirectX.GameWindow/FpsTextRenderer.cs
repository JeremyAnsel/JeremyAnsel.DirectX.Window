// <copyright file="FpsTextRenderer.cs" company="Jérémy Ansel">
// Copyright (c) 2015-2026 Jérémy Ansel
// </copyright>

using JeremyAnsel.DirectX.D2D1;
using JeremyAnsel.DirectX.D3D11;
using JeremyAnsel.DirectX.DWrite;
using JeremyAnsel.DirectX.DXCommon;
using JeremyAnsel.DirectX.Dxgi;
using JeremyAnsel.DirectX.Window;
using System.Text;

namespace JeremyAnsel.DirectX.GameWindow
{
    public sealed class FpsTextRenderer : IGameComponent
    {
        private DeviceResources? deviceResources;

        private readonly WindowPerformanceTime performanceTime;

        private D2D1DrawingStateBlock? stateBlock;

        private DxgiAdapter4? dxgiAdapter4;

        private DWriteTextFormat? textFormat;

        private D2D1SolidColorBrush? whiteBrush;

        private DWriteTextLayout? textLayout;

        private readonly StringBuilder text;

        private bool isInitialized;

        public FpsTextRenderer(WindowPerformanceTime? performanceTime)
        {
            this.performanceTime = performanceTime ?? throw new ArgumentNullException(nameof(performanceTime));

            this.text = new StringBuilder();

            this.IsEnabled = true;
            this.ShowTime = true;
            this.ShowPerformanceTime = true;
            this.ShowMemoryUsage = true;
            this.ShowAllocatedMemory = false;
        }

        public D3D11FeatureLevel MinimalFeatureLevel { get { return D3D11FeatureLevel.FeatureLevel91; } }

        public bool IsEnabled { get; set; }

        public bool ShowTime { get; set; }

        public bool ShowPerformanceTime { get; set; }

        public bool ShowMemoryUsage { get; set; }

        public bool ShowAllocatedMemory { get; set; }

        public void CreateDeviceDependentResources(DeviceResources? resources)
        {
            this.deviceResources = resources ?? throw new ArgumentNullException(nameof(resources));
            this.stateBlock = this.deviceResources.D2DFactory?.CreateDrawingStateBlock();

            using (DxgiDevice2 dxgiDevice = DxgiDevice2.CreateDeviceFromDevice(deviceResources.D3DDevice!))
            using (DxgiAdapter2 dxgiAdapter = dxgiDevice.GetAdapter())
            {
                dxgiAdapter4 = DxgiAdapter4.FromAdapter(dxgiAdapter);
            }

            this.textFormat = this.deviceResources.DWriteFactory?.CreateTextFormat("Segoe UI", null, DWriteFontWeight.Light, DWriteFontStyle.Normal, DWriteFontStretch.Normal, 18.0f, string.Empty);
        }

        public void ReleaseDeviceDependentResources()
        {
            DXUtils.DisposeAndNull(ref this.stateBlock);
            DXUtils.DisposeAndNull(ref this.textFormat);
            DXUtils.DisposeAndNull(ref this.dxgiAdapter4);
        }

        public void CreateWindowSizeDependentResources()
        {
            if (this.deviceResources is null)
            {
                return;
            }

            this.whiteBrush = this.deviceResources.D2DRenderTarget?.CreateSolidColorBrush(new D2D1ColorF(D2D1KnownColor.White));
        }

        public void ReleaseWindowSizeDependentResources()
        {
            DXUtils.DisposeAndNull(ref this.textLayout);
            DXUtils.DisposeAndNull(ref this.whiteBrush);
        }

        public void Update(ITimer? timer)
        {
            if (!this.IsEnabled)
            {
                return;
            }

            if (timer is null)
            {
                throw new ArgumentNullException(nameof(timer));
            }

            var time = timer.TotalTime;
            uint fps = timer.FramesPerSecond;

            text.Clear();

            if (this.ShowPerformanceTime)
            {
                text.Append("E:");
                text.Append(this.performanceTime.EventTime);
                text.Append(", U:");
                text.Append(this.performanceTime.UpdateTime);
                text.Append(", R:");
                text.Append(this.performanceTime.RenderTime);
                text.Append(", P:");
                text.Append(this.performanceTime.PresentTime);
                text.Append("\n");
            }

            if (this.ShowTime)
            {
                text.Append(time);
                text.Append("\n");
            }

            if (fps > 0)
            {
                text.Append(fps);
            }
            else
            {
                text.Append("-");
            }

            text.Append(" FPS\n");

            if (this.ShowAllocatedMemory)
            {
#if !NETSTANDARD
                if (timer.TotalSeconds > _allocatedMemoryTime + 1)
                {
                    _allocatedMemoryTime = timer.TotalSeconds;
                    long allocated = GC.GetAllocatedBytesForCurrentThread();
                    _allocatedMemoryCurrent = allocated - _allocatedMemoryLast;
                    _allocatedMemoryLast = allocated;
                    uint frames = timer.FrameCount - _allocatedMemoryFrames;
                    if (frames != 0)
                    {
                        _allocatedMemoryCurrent /= frames;
                    }
                    _allocatedMemoryFrames = timer.FrameCount;
                }

                text.Append("Allocated ");
                text.Append(DXUtils.StrFormatByteSize(_allocatedMemoryCurrent));
                text.Append("\n");
#endif
            }

            if (this.ShowMemoryUsage)
            {
                if (dxgiAdapter4 is not null)
                {
                    DXProcessMemoryCounters cpuMemoryInfo = DXProcessMemoryCounters.GetCurrent();
                    DxgiQueryVideoMemoryInfo gpuMemoryInfo = dxgiAdapter4.QueryVideoMemoryInfo();

                    text.Append("RAM ");
                    text.Append(DXUtils.StrFormatByteSize((long)cpuMemoryInfo.WorkingSetSize));
                    text.Append("\n");
                    text.Append("GPU ");
                    text.Append(DXUtils.StrFormatByteSize((long)gpuMemoryInfo.CurrentUsage));
                    text.Append("\n");
                }
            }

            DXUtils.DisposeAndNull(ref this.textLayout);

            this.textLayout = this.deviceResources?.DWriteFactory?.CreateTextLayout(
                text.ToString(),
                this.textFormat,
                this.deviceResources.ConvertPixelsToDipsX(600),
                this.deviceResources.ConvertPixelsToDipsY(400));

            this.isInitialized = true;
        }

#if !NETSTANDARD
        private double _allocatedMemoryTime = 0;
        private long _allocatedMemoryLast = 0;
        private long _allocatedMemoryCurrent = 0;
        private uint _allocatedMemoryFrames = 0;
#endif

        public void Render()
        {
            if (!this.IsEnabled || !this.isInitialized)
            {
                return;
            }

            if (this.deviceResources is null)
            {
                return;
            }

            var context = this.deviceResources.D2DRenderTarget;

            if (context is null)
            {
                return;
            }

            context.SaveDrawingState(this.stateBlock);
            context.BeginDraw();

            context.DrawTextLayout(new D2D1Point2F(), this.textLayout, this.whiteBrush);

            context.EndDrawIgnoringRecreateTargetError();
            context.RestoreDrawingState(this.stateBlock);
        }
    }
}

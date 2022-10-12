// <copyright file="DpiHelpers.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>

namespace JeremyAnsel.DirectX.Window
{
    using System;
    using System.Runtime.InteropServices;

    internal static class DpiHelpers
    {
        public static bool IsDpiAware()
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return false;
            }

            return NativeMethods.IsProcessDPIAware();
        }

        public static bool SetDpiAware()
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return false;
            }

            if (IsDpiAware())
            {
                return false;
            }

            bool isDpiSet = false;

            if (!isDpiSet)
            {
                try
                {
                    if (NativeMethods.SetProcessDpiAwarenessContext(NativeMethods.DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE))
                    {
                        isDpiSet = true;
                    }
                }
                catch
                {
                }
            }

            if (!isDpiSet)
            {
                try
                {
                    if (NativeMethods.SetProcessDpiAwareness(NativeMethods.PROCESS_PER_MONITOR_DPI_AWARE) == IntPtr.Zero)
                    {
                        isDpiSet = true;
                    }
                }
                catch
                {
                }
            }

            if (!isDpiSet)
            {
                try
                {
                    if (NativeMethods.SetProcessDPIAware())
                    {
                        isDpiSet = true;
                    }
                }
                catch
                {
                }
            }

            return isDpiSet;
        }
    }
}

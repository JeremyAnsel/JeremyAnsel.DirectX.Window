// <copyright file="KeyboardModifiers.cs" company="Jérémy Ansel">
// Copyright (c) 2015-2026 Jérémy Ansel
// </copyright>

namespace JeremyAnsel.DirectX.Window
{
    public static class KeyboardModifiers
    {
        private static bool IsKeyDown(VirtualKey key)
        {
            return (NativeMethods.GetKeyState((int)key) & 0x8000) != 0;
        }

        public static bool IsControlDown => IsKeyDown(VirtualKey.Control);

        public static bool IsLeftControlDown => IsKeyDown(VirtualKey.LeftControl);

        public static bool IsRightControlDown => IsKeyDown(VirtualKey.RightControl);

        public static bool IsShiftDown => IsKeyDown(VirtualKey.Shift);

        public static bool IsLeftShiftDown => IsKeyDown(VirtualKey.LeftShift);

        public static bool IsRightShiftDown => IsKeyDown(VirtualKey.RightShift);

        public static bool IsAltDown => IsKeyDown(VirtualKey.Menu);

        public static bool IsLeftAltDown => IsKeyDown(VirtualKey.LeftMenu);

        public static bool IsRightAltDown => IsKeyDown(VirtualKey.RightMenu);
    }
}

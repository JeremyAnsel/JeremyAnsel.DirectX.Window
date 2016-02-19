// <copyright file="ClassStyles.cs" company="Jérémy Ansel">
// Copyright (c) 2015 Jérémy Ansel
// </copyright>

namespace JeremyAnsel.DirectX.Window
{
    using System;

    [Flags]
    internal enum ClassStyles
    {
        VerticalRedraw = 0x0001,

        HorizontalRedraw = 0x0002,

        DoubleClicks = 0x0008,

        OwnDC = 0x0020,

        ClassDC = 0x0040,

        ParentDC = 0x0080,

        NoClose = 0x0200,

        SaveBits = 0x0800,

        ByteAlignClient = 0x1000,

        ByteAlignWindow = 0x2000,

        GlobalClass = 0x4000,

        DropShadow = 0x00020000
    }
}

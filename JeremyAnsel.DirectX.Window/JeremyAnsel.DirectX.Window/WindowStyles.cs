// <copyright file="WindowStyles.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>

namespace JeremyAnsel.DirectX.Window
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue", Justification = "Reviewed")]
    [SuppressMessage("Microsoft.Usage", "CA2217:DoNotMarkEnumsWithFlags", Justification = "Reviewed")]
    [Flags]
    internal enum WindowStyles
    {
        Overlapped = 0x00000000,

        Popup = unchecked((int)0x80000000),

        Child = 0x40000000,

        Minimize = 0x20000000,

        Visible = 0x10000000,

        Disabled = 0x08000000,

        ClipSiblings = 0x04000000,

        ClipChildren = 0x02000000,

        Maximize = 0x01000000,

        Caption = 0x00C00000,

        Border = 0x00800000,

        DialogFrame = 0x00400000,

        VerticalScroll = 0x00200000,

        HorizontalScroll = 0x00100000,

        SystemMenu = 0x00080000,

        ThickFrame = 0x00040000,

        Group = 0x00020000,

        TabStop = 0x00010000,

        MinimizeBox = 0x00020000,

        MaximizeBox = 0x00010000,

        OverlappedWindow = Overlapped | Caption | SystemMenu | ThickFrame | MinimizeBox | MaximizeBox,

        PopupWindow = Popup | Border | SystemMenu,

        ChildWindow = Child
    }
}

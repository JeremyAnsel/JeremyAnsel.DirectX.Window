// <copyright file="VirtualKey.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>

namespace JeremyAnsel.DirectX.Window
{
    using System.Diagnostics.CodeAnalysis;

    public enum VirtualKey
    {
        None = 0,

        LeftButton = 0x01,

        RightButton = 0x02,

        Cancel = 0x03,

        MiddleButton = 0x04,

        XButton1 = 0x05,

        XButton2 = 0x06,

        Back = 0x08,

        Tab = 0x09,

        Clear = 0x0C,

        Return = 0x0D,

        Shift = 0x10,

        Control = 0x11,

        Menu = 0x12,

        Pause = 0x13,

        Capital = 0x14,

        Escape = 0x1B,

        Space = 0x20,

        Prior = 0x21,

        Next = 0x22,

        End = 0x23,

        Home = 0x24,

        Left = 0x25,

        Up = 0x26,

        Right = 0x27,

        Down = 0x28,

        Select = 0x29,

        Print = 0x2A,

        Execute = 0x2B,

        Snapshot = 0x2C,

        Insert = 0x2D,

        Delete = 0x2E,

        Help = 0x2F,

        D0 = 0x30,

        D1,

        D2,

        D3,

        D4,

        D5,

        D6,

        D7,

        D8,

        D9,

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "A", Justification="Reviewed")]
        A = 0x41,

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "B", Justification = "Reviewed")]
        B,

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "C", Justification = "Reviewed")]
        C,

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "D", Justification = "Reviewed")]
        D,

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "E", Justification = "Reviewed")]
        E,

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "F", Justification = "Reviewed")]
        F,

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "G", Justification = "Reviewed")]
        G,

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "H", Justification = "Reviewed")]
        H,

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "I", Justification = "Reviewed")]
        I,

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "J", Justification = "Reviewed")]
        J,

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "K", Justification = "Reviewed")]
        K,

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "L", Justification = "Reviewed")]
        L,

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "M", Justification = "Reviewed")]
        M,

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "N", Justification = "Reviewed")]
        N,

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "O", Justification = "Reviewed")]
        O,

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "P", Justification = "Reviewed")]
        P,

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Q", Justification = "Reviewed")]
        Q,

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "R", Justification = "Reviewed")]
        R,

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "S", Justification = "Reviewed")]
        S,

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "T", Justification = "Reviewed")]
        T,

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "U", Justification = "Reviewed")]
        U,

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "V", Justification = "Reviewed")]
        V,

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "W", Justification = "Reviewed")]
        W,

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "X", Justification = "Reviewed")]
        X,

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Y", Justification = "Reviewed")]
        Y,

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Z", Justification = "Reviewed")]
        Z,

        LeftWindow = 0x5B,

        RightWindow = 0x5C,

        Applications = 0x5D,

        Sleep = 0x5F,

        NumPad0 = 0x60,

        NumPad1 = 0x61,

        NumPad2 = 0x62,

        NumPad3 = 0x63,

        NumPad4 = 0x64,

        NumPad5 = 0x65,

        NumPad6 = 0x66,

        NumPad7 = 0x67,

        NumPad8 = 0x68,

        NumPad9 = 0x69,

        Multiply = 0x6A,

        Add = 0x6B,

        Separator = 0x6C,

        Subtract = 0x6D,

#pragma warning disable CA1720 // L'identificateur contient le nom de type
        Decimal = 0x6E,
#pragma warning restore CA1720 // L'identificateur contient le nom de type

        Divide = 0x6F,

        F1 = 0x70,

        F2 = 0x71,

        F3 = 0x72,

        F4 = 0x73,

        F5 = 0x74,

        F6 = 0x75,

        F7 = 0x76,

        F8 = 0x77,

        F9 = 0x78,

        F10 = 0x79,

        F11 = 0x7A,

        F12 = 0x7B,

        F13 = 0x7C,

        F14 = 0x7D,

        F15 = 0x7E,

        F16 = 0x7F,

        F17 = 0x80,

        F18 = 0x81,

        F19 = 0x82,

        F20 = 0x83,

        F21 = 0x84,

        F22 = 0x85,

        F23 = 0x86,

        F24 = 0x87,

        NumLock = 0x90,

        Scroll = 0x91,

        LeftShift = 0xA0,

        RightShift = 0xA1,

        LeftControl = 0xA2,

        RightControl = 0xA3,

        LeftMenu = 0xA4,

        RightMenu = 0xA5,

        BrowserBack = 0xA6,

        BrowserForward = 0xA7,

        BrowserRefresh = 0xA8,

        BrowserStop = 0xA9,

        BrowserSearch = 0xAA,

        BrowserFavorites = 0xAB,

        BrowserHome = 0xAC,

        VolumeMute = 0xAD,

        VolumeDown = 0xAE,

        VolumeUp = 0xAF,

        MediaNextTrack = 0xB0,

        MediaPrevTrack = 0xB1,

        MediaStop = 0xB2,

        MediaPlayPause = 0xB3,

        ProcessKey = 0xE5,

        Packet = 0xE7,

        Attn = 0xF6,

        CRSel = 0xF7,

        ExSel = 0xF8,

        EraseEof = 0xF9,

        Play = 0xFA,

        Zoom = 0xFB,

        PA1 = 0xFD,
    }
}

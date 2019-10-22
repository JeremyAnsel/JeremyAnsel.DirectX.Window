// <copyright file="MessageBoxDefaultButton.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>

namespace JeremyAnsel.DirectX.Window
{
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("Microsoft.Design", "CA1027:MarkEnumsWithFlags", Justification = "Reviewed")]
    public enum MessageBoxDefaultButton
    {
        Button1 = 0x00000000,

        Button2 = 0x00000100,

        Button3 = 0x00000200,

        Button4 = 0x00000300,
    }
}

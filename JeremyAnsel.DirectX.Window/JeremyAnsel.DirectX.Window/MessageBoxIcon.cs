// <copyright file="MessageBoxIcon.cs" company="Jérémy Ansel">
// Copyright (c) 2015 Jérémy Ansel
// </copyright>

namespace JeremyAnsel.DirectX.Window
{
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("Microsoft.Design", "CA1027:MarkEnumsWithFlags", Justification = "Reviewed")]
    public enum MessageBoxIcon
    {
        None = 0,

        Hand = 0x00000010,

        Question = 0x00000020,

        Exclamation = 0x00000030,

        Asterisk = 0x00000040,

        Warning = MessageBoxIcon.Exclamation,

        Error = MessageBoxIcon.Hand,

        Information = MessageBoxIcon.Asterisk,

        Stop = MessageBoxIcon.Hand
    }
}

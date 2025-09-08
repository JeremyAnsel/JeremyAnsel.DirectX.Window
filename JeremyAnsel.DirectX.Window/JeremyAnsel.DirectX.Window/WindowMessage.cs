// <copyright file="WindowMessage.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>

namespace JeremyAnsel.DirectX.Window
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct WindowMessage : IEquatable<WindowMessage>
    {
        private readonly IntPtr handle;

        private readonly WindowMessageType msg;

        private readonly IntPtr lParam;

        private readonly IntPtr wParam;

        private readonly uint time;

        private readonly int x;

        private readonly int y;

        public IntPtr Handle
        {
            get { return this.handle; }
        }

        public WindowMessageType Msg
        {
            get { return this.msg; }
        }

        public IntPtr LParam
        {
            get { return this.lParam; }
        }

        public IntPtr WParam
        {
            get { return this.wParam; }
        }

        public uint Time
        {
            get { return this.time; }
        }

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "X", Justification = "Reviewed")]
        public int X
        {
            get { return this.x; }
        }

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Y", Justification = "Reviewed")]
        public int Y
        {
            get { return this.y; }
        }

        /// <summary>
        /// Compares two <see cref="WindowMessage"/> objects. The result specifies whether the values of the two objects are equal.
        /// </summary>
        /// <param name="left">The left <see cref="WindowMessage"/> to compare.</param>
        /// <param name="right">The right <see cref="WindowMessage"/> to compare.</param>
        /// <returns><value>true</value> if the values of left and right are equal; otherwise, <value>false</value>.</returns>
        public static bool operator ==(WindowMessage left, WindowMessage right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares two <see cref="WindowMessage"/> objects. The result specifies whether the values of the two objects are unequal.
        /// </summary>
        /// <param name="left">The left <see cref="WindowMessage"/> to compare.</param>
        /// <param name="right">The right <see cref="WindowMessage"/> to compare.</param>
        /// <returns><value>true</value> if the values of left and right differ; otherwise, <value>false</value>.</returns>
        public static bool operator !=(WindowMessage left, WindowMessage right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><value>true</value> if the specified object is equal to the current object; otherwise, <value>false</value>.</returns>
        public override bool Equals(object? obj)
        {
            if (obj is not WindowMessage)
            {
                return false;
            }

            return this.Equals((WindowMessage)obj);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns><value>true</value> if the specified object is equal to the current object; otherwise, <value>false</value>.</returns>
        public bool Equals(WindowMessage other)
        {
            return this.handle == other.handle
                && this.msg == other.msg
                && this.lParam == other.lParam
                && this.wParam == other.wParam
                && this.time == other.time
                && this.x == other.x
                && this.y == other.y;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            return new
            {
                this.handle,
                this.msg,
                this.lParam,
                this.wParam,
                this.time,
                this.x,
                this.y
            }
            .GetHashCode();
        }
    }
}

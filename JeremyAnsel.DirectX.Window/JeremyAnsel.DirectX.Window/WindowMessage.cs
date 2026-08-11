// <copyright file="WindowMessage.cs" company="Jérémy Ansel">
// Copyright (c) 2015-2026 Jérémy Ansel
// </copyright>

using System.Diagnostics.CodeAnalysis;

namespace JeremyAnsel.DirectX.Window
{
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

        public static bool operator ==(WindowMessage left, WindowMessage right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(WindowMessage left, WindowMessage right)
        {
            return !(left == right);
        }

        public override bool Equals(object? obj)
        {
            return obj is WindowMessage message && Equals(message);
        }

        public bool Equals(WindowMessage other)
        {
            return EqualityComparer<IntPtr>.Default.Equals(handle, other.handle) &&
                   msg == other.msg &&
                   EqualityComparer<IntPtr>.Default.Equals(lParam, other.lParam) &&
                   EqualityComparer<IntPtr>.Default.Equals(wParam, other.wParam) &&
                   time == other.time &&
                   x == other.x &&
                   y == other.y;
        }

        public override int GetHashCode()
        {
            int hashCode = 123658995;
            hashCode = hashCode * -1521134295 + handle.GetHashCode();
            hashCode = hashCode * -1521134295 + msg.GetHashCode();
            hashCode = hashCode * -1521134295 + lParam.GetHashCode();
            hashCode = hashCode * -1521134295 + wParam.GetHashCode();
            hashCode = hashCode * -1521134295 + time.GetHashCode();
            hashCode = hashCode * -1521134295 + x.GetHashCode();
            hashCode = hashCode * -1521134295 + y.GetHashCode();
            return hashCode;
        }
    }
}

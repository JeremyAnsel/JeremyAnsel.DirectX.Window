// <copyright file="ClassInfoEx.cs" company="Jérémy Ansel">
// Copyright (c) 2015, 2019 Jérémy Ansel
// </copyright>

namespace JeremyAnsel.DirectX.Window
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct ClassInfoEx : IEquatable<ClassInfoEx>
    {
        private readonly uint size;

        private readonly ClassStyles style;

        private readonly IntPtr windowProcedure;

        private readonly uint classExtra;

        private readonly uint windowExtra;

        private readonly IntPtr instance;

        private readonly IntPtr icon;

        private readonly IntPtr cursor;

        private readonly IntPtr brushBackground;

        [MarshalAs(UnmanagedType.LPWStr)]
        private readonly string? menuName;

        [MarshalAs(UnmanagedType.LPWStr)]
        private readonly string className;

        private readonly IntPtr iconSmall;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ClassInfoEx(
            ClassStyles style,
            IntPtr windowProcedure,
            IntPtr instance,
            IntPtr icon,
            IntPtr cursor,
            IntPtr brushBackground,
            string? menuName,
            string className,
            IntPtr iconSmall)
        {
            this.size = (uint)Marshal.SizeOf(typeof(ClassInfoEx));
            this.style = style;
            this.windowProcedure = windowProcedure;
            this.classExtra = 0;
            this.windowExtra = 0;
            this.instance = instance;
            this.icon = icon;
            this.cursor = cursor;
            this.brushBackground = brushBackground;
            this.menuName = menuName;
            this.className = className;
            this.iconSmall = iconSmall;
        }

        public ClassStyles Style
        {
            get { return this.style; }
        }

        public IntPtr WindowProcedure
        {
            get { return this.windowProcedure; }
        }

        public uint ClassExtra
        {
            get { return this.classExtra; }
        }

        public uint WindowExtra
        {
            get { return this.windowExtra; }
        }

        public IntPtr Instance
        {
            get { return this.instance; }
        }

        public IntPtr Icon
        {
            get { return this.icon; }
        }

        public IntPtr Cursor
        {
            get { return this.cursor; }
        }

        public IntPtr BrushBackground
        {
            get { return this.brushBackground; }
        }

        public string? MenuName
        {
            get { return this.menuName; }
        }

        public string ClassName
        {
            get { return this.className; }
        }

        public IntPtr IconSmall
        {
            get { return this.iconSmall; }
        }

        /// <summary>
        /// Compares two <see cref="ClassInfoEx"/> objects. The result specifies whether the values of the two objects are equal.
        /// </summary>
        /// <param name="left">The left <see cref="ClassInfoEx"/> to compare.</param>
        /// <param name="right">The right <see cref="ClassInfoEx"/> to compare.</param>
        /// <returns><value>true</value> if the values of left and right are equal; otherwise, <value>false</value>.</returns>
        public static bool operator ==(ClassInfoEx left, ClassInfoEx right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares two <see cref="ClassInfoEx"/> objects. The result specifies whether the values of the two objects are unequal.
        /// </summary>
        /// <param name="left">The left <see cref="ClassInfoEx"/> to compare.</param>
        /// <param name="right">The right <see cref="ClassInfoEx"/> to compare.</param>
        /// <returns><value>true</value> if the values of left and right differ; otherwise, <value>false</value>.</returns>
        public static bool operator !=(ClassInfoEx left, ClassInfoEx right)
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
            if (obj is not ClassInfoEx)
            {
                return false;
            }

            return this.Equals((ClassInfoEx)obj);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns><value>true</value> if the specified object is equal to the current object; otherwise, <value>false</value>.</returns>
        public bool Equals(ClassInfoEx other)
        {
            return this.size == other.size
                && this.style == other.style
                && this.windowProcedure == other.windowProcedure
                && this.classExtra == other.classExtra
                && this.windowExtra == other.windowExtra
                && this.instance == other.instance
                && this.icon == other.icon
                && this.cursor == other.cursor
                && this.brushBackground == other.brushBackground
                && this.menuName == other.menuName
                && this.className == other.className
                && this.iconSmall == other.iconSmall;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            return new
            {
                this.size,
                this.style,
                this.windowProcedure,
                this.classExtra,
                this.windowExtra,
                this.instance,
                this.icon,
                this.cursor,
                this.brushBackground,
                this.menuName,
                this.className,
                this.iconSmall
            }
            .GetHashCode();
        }
    }
}

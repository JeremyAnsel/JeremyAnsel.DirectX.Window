// <copyright file="ClassInfoEx.cs" company="Jérémy Ansel">
// Copyright (c) 2015-2026 Jérémy Ansel
// </copyright>

using System.Runtime.CompilerServices;

namespace JeremyAnsel.DirectX.Window
{
    internal unsafe readonly struct ClassInfoEx : IEquatable<ClassInfoEx>
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

        private readonly char* menuName;

        private readonly char* className;

        private readonly IntPtr iconSmall;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ClassInfoEx(
            ClassStyles style,
            IntPtr windowProcedure,
            IntPtr instance,
            IntPtr icon,
            IntPtr cursor,
            IntPtr brushBackground,
            char* menuName,
            char* className,
            IntPtr iconSmall)
        {
            this.size = (uint)sizeof(ClassInfoEx);
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

        public char* MenuName
        {
            get { return this.menuName; }
        }

        public char* ClassName
        {
            get { return this.className; }
        }

        public IntPtr IconSmall
        {
            get { return this.iconSmall; }
        }

        public static bool operator ==(ClassInfoEx left, ClassInfoEx right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ClassInfoEx left, ClassInfoEx right)
        {
            return !(left == right);
        }

        public override bool Equals(object? obj)
        {
            return obj is ClassInfoEx ex && Equals(ex);
        }

        public bool Equals(ClassInfoEx other)
        {
            return size == other.size &&
                   style == other.style &&
                   EqualityComparer<IntPtr>.Default.Equals(windowProcedure, other.windowProcedure) &&
                   classExtra == other.classExtra &&
                   windowExtra == other.windowExtra &&
                   instance == other.instance &&
                   icon == other.icon &&
                   cursor == other.cursor &&
                   brushBackground == other.brushBackground &&
                   menuName == other.menuName &&
                   className == other.className &&
                   iconSmall == other.iconSmall;
        }

        public override int GetHashCode()
        {
            int hashCode = 1466023840;
            hashCode = hashCode * -1521134295 + size.GetHashCode();
            hashCode = hashCode * -1521134295 + style.GetHashCode();
            hashCode = hashCode * -1521134295 + windowProcedure.GetHashCode();
            hashCode = hashCode * -1521134295 + classExtra.GetHashCode();
            hashCode = hashCode * -1521134295 + windowExtra.GetHashCode();
            hashCode = hashCode * -1521134295 + instance.GetHashCode();
            hashCode = hashCode * -1521134295 + icon.GetHashCode();
            hashCode = hashCode * -1521134295 + cursor.GetHashCode();
            hashCode = hashCode * -1521134295 + brushBackground.GetHashCode();
            hashCode = hashCode * -1521134295 + ((nint)menuName).GetHashCode();
            hashCode = hashCode * -1521134295 + ((nint)className).GetHashCode();
            hashCode = hashCode * -1521134295 + iconSmall.GetHashCode();
            return hashCode;
        }
    }
}

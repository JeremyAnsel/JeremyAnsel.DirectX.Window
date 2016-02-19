// <copyright file="MessageBox.cs" company="Jérémy Ansel">
// Copyright (c) 2015 Jérémy Ansel
// </copyright>

namespace JeremyAnsel.DirectX.Window
{
    using System;

    public static class MessageBox
    {
        public static MessageBoxResult Show(string text)
        {
            return MessageBox.Show(IntPtr.Zero, text, string.Empty, MessageBoxButton.Ok, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
        }

        public static MessageBoxResult Show(IntPtr handle, string text)
        {
            return MessageBox.Show(handle, text, string.Empty, MessageBoxButton.Ok, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
        }

        public static MessageBoxResult Show(string text, string caption)
        {
            return MessageBox.Show(IntPtr.Zero, text, caption, MessageBoxButton.Ok, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
        }

        public static MessageBoxResult Show(IntPtr handle, string text, string caption)
        {
            return MessageBox.Show(handle, text, caption, MessageBoxButton.Ok, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
        }

        public static MessageBoxResult Show(string text, string caption, MessageBoxButton button)
        {
            return MessageBox.Show(IntPtr.Zero, text, caption, button, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
        }

        public static MessageBoxResult Show(IntPtr handle, string text, string caption, MessageBoxButton button)
        {
            return MessageBox.Show(handle, text, caption, button, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
        }

        public static MessageBoxResult Show(string text, string caption, MessageBoxButton button, MessageBoxIcon icon)
        {
            return MessageBox.Show(IntPtr.Zero, text, caption, button, icon, MessageBoxDefaultButton.Button1);
        }

        public static MessageBoxResult Show(IntPtr handle, string text, string caption, MessageBoxButton button, MessageBoxIcon icon)
        {
            return MessageBox.Show(handle, text, caption, button, icon, MessageBoxDefaultButton.Button1);
        }

        public static MessageBoxResult Show(string text, string caption, MessageBoxButton button, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
        {
            return MessageBox.Show(IntPtr.Zero, text, caption, button, icon, defaultButton);
        }

        public static MessageBoxResult Show(IntPtr handle, string text, string caption, MessageBoxButton button, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
        {
            return NativeMethods.MessageBox(handle, text, caption, (uint)button | (uint)icon | (uint)defaultButton);
        }
    }
}

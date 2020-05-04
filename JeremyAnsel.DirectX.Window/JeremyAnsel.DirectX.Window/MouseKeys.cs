using System;
using System.Collections.Generic;
using System.Text;

namespace JeremyAnsel.DirectX.Window
{
    /// <summary>
    /// Indicates whether various virtual keys are down.
    /// </summary>
    [Flags]
    public enum MouseKeys
    {
        /// <summary>
        /// The left mouse button is down.
        /// </summary>
        LeftButton = 0x0001,

        /// <summary>
        /// The right mouse button is down.
        /// </summary>
        RightButton = 0x0002,

        /// <summary>
        /// The SHIFT key is down.
        /// </summary>
        Shift = 0x0004,

        /// <summary>
        /// The CTRL key is down.
        /// </summary>
        Control = 0x0008,

        /// <summary>
        /// The middle mouse button is down.
        /// </summary>
        MiddleButton = 0x0010,

        /// <summary>
        /// The first X button is down.
        /// </summary>
        XButton1 = 0x0020,

        /// <summary>
        /// The second X button is down.
        /// </summary>
        XButton2 = 0x0040,
    }
}

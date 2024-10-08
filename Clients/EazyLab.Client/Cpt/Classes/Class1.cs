﻿using System;
using System.Runtime.InteropServices;

namespace EazyLab.Classes
{
    //Add below codes in Form constructor for avoid form flickering.

    //int style = NativeWinAPI.GetWindowLong(this.Handle, NativeWinAPI.GWL_EXSTYLE);

    //style |= NativeWinAPI.WS_EX_COMPOSITED; 

    //NativeWinAPI.SetWindowLong(this.Handle, NativeWinAPI.GWL_EXSTYLE, style);

    /// <summary>
    /// For avoid flickering Form
    /// </summary>

    internal static class NativeWinAPI
    {

        internal static readonly int GWL_EXSTYLE = -20;

        internal static readonly int WS_EX_COMPOSITED = 0x02000000;

        [DllImport("user32")]
        internal static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32")]
        internal static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

    }

}

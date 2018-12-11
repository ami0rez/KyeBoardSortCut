using System;
using System.Runtime.InteropServices;

namespace ShortCuts
{
    /// <summary>
    /// Invoked Methodes
    /// </summary>
    class NativeMethods
    {
        //imprts GetAsynkKeyState
        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(Int32 i);
    }
}

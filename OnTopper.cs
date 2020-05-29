using OnTopperLib.Exception;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace OnTopperLib
{
    public class OnTopper
    {
        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        private const uint SWP_NOSIZE = 0x0001;
        private const uint SWP_NOMOVE = 0x0002;
        private const uint SWP_SHOWWINDOW = 0x0040;

        public enum WINDOW_STATE { TOP, UNTOP }

        public static void SetWindowState(Process p, WINDOW_STATE state)
        {
            try
            {
                if (state == WINDOW_STATE.TOP)
                {
                    SetWindowPos(GetWindowHandle(p), HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_SHOWWINDOW);
                }
                else
                {
                    SetWindowPos(GetWindowHandle(p), HWND_NOTOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_SHOWWINDOW);
                }
            }
            catch (ArgumentException)
            {
                throw new ProcessNotExistsException("Process doesnt exists");
            }
        }

        private static IntPtr GetWindowHandle(Process p) => Process.GetProcessById(p.Id).MainWindowHandle;

        public static bool HasMainWindow(Process p) => p.MainWindowHandle != IntPtr.Zero;
    }
}

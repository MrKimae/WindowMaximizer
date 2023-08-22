using System.Runtime.InteropServices;

namespace WindowMaximizer
{
    class User32
    {
        /* Window field offsets for GetWindowLong()*/
        public const long GWL_WNDPROC = -4L;
        public const long GWL_HINSTANCE = -6L;
        public const long GWL_HWNDPARENT = -8L;
        public const long GWL_STYLE = -16L;
        public const long GWL_EXSTYLE = -20L;
        public const long GWL_USERDATA = -21L;
        public const long GWL_ID = -12L;

        /* SetWindowPos Flags*/
        public const long SWP_NOSIZE = 0x0001;
        public const long SWP_NOMOVE = 0x0002;
        public const long SWP_NOZORDER = 0x0004;
        public const long SWP_NOREDRAW = 0x0008;
        public const long SWP_NOACTIVATE = 0x0010;	/* nyi*/
        public const long SWP_FRAMECHANGED = 0x0020;	/* nyi*/
        public const long SWP_SHOWWINDOW = 0x0040;
        public const long SWP_HIDEWINDOW = 0x0080;
        public const long SWP_NOCOPYBITS = 0x0100;	/* nyi*/
        public const long SWP_NOOWNERZORDER = 0x0200;	/* nyi*/
        public const long SWP_NOSENDCHANGING = 0x0400;	/* nyi*/
        public const long SWP_DRAWFRAME = SWP_FRAMECHANGED;
        public const long SWP_NOREPOSITION = SWP_NOOWNERZORDER;
        public const long SWP_DEFERERASE = 0x2000;	/* nyi*/
        public const long SWP_ASYNCWINDOWPOS = 0x4000;	/* nyi*/

        /* ShowWindow() Commands */
        public const long SW_HIDE = 0;
        public const long SW_SHOWNORMAL = 1;
        public const long SW_NORMAL = 1;
        public const long SW_SHOWMINIMIZED = 2;
        public const long SW_SHOWMAXIMIZED = 3;
        public const long SW_MAXIMIZE = 3;
        public const long SW_SHOWNOACTIVATE = 4;
        public const long SW_SHOW = 5;
        public const long SW_MINIMIZE = 6;
        public const long SW_SHOWMINNOACTIVE = 7;
        public const long SW_SHOWNA = 8;
        public const long SW_RESTORE = 9;
        public const long SW_SHOWDEFAULT = 10;
        public const long SW_FORCEMINIMIZE = 11;
        public const long SW_MAX = 11;

        /* GetSystemMetrics */
        public const int SM_CXSCREEN = 0;
        public const int SM_CYSCREEN = 1;

        [DllImport("user32.dll")]
        public static extern bool GetWindowPlacement(int hWnd, ref WINDOWPLACEMENT lpwndpl);

        [DllImport("user32.dll", EntryPoint = "GetWindowLongA")]
        public static extern long GetWindowLong(long hwnd, long nIndex);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongA")]
        public static extern long SetWindowLong(long hwnd, long nIndex, long dwNewLong);

        [DllImport("user32.dll")]
        public static extern long SetWindowPos(long hwnd, long hWndInsertAfter, long x, long y, long cx, long cy, long wFlags);

        [DllImport("User32.dll")]
        public static extern int FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "GetSystemMetrics")]
        public static extern int GetSystemMetrics(int which);

        [DllImport("User32.dll")]
        public static extern bool ShowWindow(int hwnd, long nCmdShow);

        #region WindowFullscreen Check
        public struct POINTAPI
        {
            public int x;
            public int y;
        }

        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        public struct WINDOWPLACEMENT
        {
            public int length;
            public int flags;
            public int showCmd;
            public POINTAPI ptMinPosition;
            public POINTAPI ptMaxPosition;
            public RECT rcNormalPosition;
        }

        public static int getWindowPlacementLength(WINDOWPLACEMENT windowPlacement)
        {
            return Marshal.SizeOf(windowPlacement);
        }
        #endregion

        #region Screen Size
        public static int ScreenX
        {
            get { return GetSystemMetrics(SM_CXSCREEN); }
        }

        public static int ScreenY
        {
            get { return GetSystemMetrics(SM_CYSCREEN); }
        }
        #endregion
    }
}

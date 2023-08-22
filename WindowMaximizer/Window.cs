namespace WindowMaximizer
{
    class Window
    {
        public static int getHandle(string lpClassName, string lpWindowName){
            return User32.FindWindow(lpClassName, lpWindowName);
        }

        public static void toggleTitleBar(long hwnd, string showTitle)
        {
            bool showTitleBool = false;
            long style = User32.GetWindowLong(hwnd, User32.GWL_STYLE);
            try
            {
                showTitleBool = bool.Parse(showTitle);
            }
            catch (System.ArgumentNullException)
            {
                showTitleBool = hasTitle(style);
            }
            if (showTitleBool)
                style |= 0xc00000L;
            else
                style &= -0xc00001L;
            // Set window style
            User32.SetWindowLong(hwnd, User32.GWL_STYLE, style);
            // Refresh window
            long wFlags = User32.SWP_FRAMECHANGED | User32.SWP_NOSIZE | User32.SWP_NOMOVE | User32.SWP_NOZORDER;
            User32.SetWindowPos(hwnd, 0L, 0L, 0L, 0L, 0L, wFlags);
        }

        public static bool hasTitle(long style)
        {
            bool hasTitle = false;
            long styleWithTitle = style & -0xc00001L;
            if (style == styleWithTitle)
            {
                hasTitle = true;
            }
            return hasTitle;
        }

        public static bool isMaximized(int hwnd)
        {
            bool isMaximized = false;
            int showCmd = getShowCmd(hwnd);

            // And now check the windowPlacement structs properties to see if window is maximised
            if (showCmd == User32.SW_MAXIMIZE) // 3 means maximised
            {
                isMaximized = true;
            }
            return isMaximized;
        }

        public static bool isNormal(int hwnd)
        {
            bool isNormal = false;
            int showCmd = getShowCmd(hwnd);

            // And now check the windowPlacement structs properties to see if window is maximised
            if (showCmd == User32.SW_NORMAL) // 1 means normal
            {
                isNormal = true;
            }
            return isNormal;
        }

        public static int getShowCmd(int hwnd)
        {
            // get the placement
            User32.WINDOWPLACEMENT windowPlacement = new User32.WINDOWPLACEMENT();
            windowPlacement.length = User32.getWindowPlacementLength(windowPlacement);
            User32.GetWindowPlacement(hwnd, ref windowPlacement);
            return windowPlacement.showCmd;
        }

        public static void setMaximized(int hwnd)
        {
            User32.SetWindowPos(hwnd, 0, 0, 0, User32.ScreenX, User32.ScreenY, User32.SWP_SHOWWINDOW);
        }

        public static void setNormal(int hwnd)
        {
            User32.ShowWindow(hwnd, User32.SW_NORMAL);
        }
    }
}

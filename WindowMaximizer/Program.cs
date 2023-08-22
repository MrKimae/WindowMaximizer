using System.Windows.Forms;
using WindowMaximizer;

public class Program
{
    public static void Main(string[] args)
    {
        string showTitle = null;
        string lpClassName = null;
        string lpWindowName = null;
        string appPath = null;
        string appArgs = null;
        foreach (string arg in args)
        {
            if (arg.ToLower().StartsWith("--showTitle=".ToLower()))
            {
                showTitle = arg.Substring("--showTitle=".Length);
            }
            if (arg.ToLower().StartsWith("--lpClassName=".ToLower()))
            {
                lpClassName = arg.Substring("--lpClassName=".Length);
            }
            if (arg.ToLower().StartsWith("--lpWindowName=".ToLower()))
            {
                lpWindowName = arg.Substring("--lpWindowName=".Length);
            }
            if (arg.ToLower().StartsWith("--appPath=".ToLower()))
            {
                appPath = arg.Substring("--appPath=".Length);
            }
            if (arg.ToLower().StartsWith("--appArgs=".ToLower()))
            {
                appArgs = arg.Substring("--appArgs=".Length);
            }
        }
        if (lpWindowName == null && lpClassName == null)
        {
            log("Please provide window name (--lpWindowName) or class name (--lpClassName) to maximize");
        }
        int hwnd = Window.getHandle(lpClassName, lpWindowName);
        if (hwnd == 0)
        {
            log(lpWindowName + " (" + lpClassName + ") window not found...");
            if (appPath != null)
            {
                log("Starting " + appPath + " " + appArgs);
                System.Diagnostics.Process.Start(appPath, appArgs);
            }
            else
            {
                log("Please provide --appPath if you want to launch an application automatically.");
            }

            log("Searching");
            while (hwnd == 0)
            {
                System.Console.Write(".");
                hwnd = Window.getHandle(lpClassName, lpWindowName);
                System.Threading.Thread.Sleep(1000);
            }
            log();

        }
        log("Found " + lpWindowName + " (" + lpClassName + ")");
        
        //If window is fullscreen, set normal
        if (!Window.isNormal(hwnd))
        {
            log("Setting window to normal view");
            Window.setNormal(hwnd);
        }
        log("Toggling TitleBar");
        Window.toggleTitleBar(hwnd, showTitle);
    }

    public static void log(string msg)
    {
        System.Console.WriteLine(msg);
    }
}

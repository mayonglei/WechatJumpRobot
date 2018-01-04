using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace WechatJumpRobot
{

    public static class ScreenSnapUtil
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;                             //最左坐标
            public int Top;                             //最上坐标
            public int Right;                           //最右坐标
            public int Bottom;                        //最下坐标
        }

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("User32.dll", EntryPoint = "FindWindowEx")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpClassName, string lpWindowName);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool SwitchToThisWindow(IntPtr hWnd, bool fAltTab);
        [DllImport("user32")]
        [return: MarshalAs(UnmanagedType.Bool)]
        //IMPORTANT : LPARAM  must be a pointer (InterPtr) in VS2005, otherwise an exception will be thrown
        private static extern bool EnumChildWindows(IntPtr window, EnumWindowProc callback, IntPtr i);
        //the callback function for the EnumChildWindows
        private delegate bool EnumWindowProc(IntPtr hWnd, IntPtr parameter);
        public static Bitmap Snap(string Title)
        {
            //IntPtr hWnd = FindWindow(null, "ecg.txt - 记事本");
            IntPtr hWnd = FindWindowEx(IntPtr.Zero, IntPtr.Zero, "CHWindow", Title);
            IntPtr hWnd1 = FindWindowEx(hWnd, IntPtr.Zero, "CHWindow", null);
            //while (hWnd1!=IntPtr.Zero)
            //{
            //    RECT rc2 = new RECT();
            //    GetWindowRect(hWnd1, ref rc2);
            //    Console.WriteLine("{0},{1}",hWnd1,rc2.Left);
            //    hWnd1 = FindWindowEx(hWnd, hWnd1, "CHWindow", null);
            //}
            //hWnd1 = FindWindowEx(hWnd, hWnd1, "CHWindow", null);
            //hWnd1 = FindWindowEx(hWnd, hWnd1, "CHWindow", null);
            //hWnd1 = FindWindowEx(hWnd, hWnd1, "CHWindow", null);
            //hWnd1 = FindWindowEx(hWnd, hWnd1, "CHWindow", null);
            //hWnd1 = FindWindowEx(hWnd, hWnd1, "CHWindow", null);
            //hWnd1 = FindWindowEx(hWnd, hWnd1, "CHWindow", null);
            //hWnd1 = FindWindowEx(hWnd, hWnd1, "CHWindow", null);
            if (hWnd1 == IntPtr.Zero)
            {
                return null;
            }
            SwitchToThisWindow(hWnd1, true);

            RECT rc = new RECT();
            GetWindowRect(hWnd1, ref rc);
            int width = rc.Right - rc.Left;                        //窗口的宽度
            int height = rc.Bottom - rc.Top;                   //窗口的高度
            int x = rc.Left;
            int y = rc.Top;

            Bitmap image = new Bitmap(width, height);
            Graphics imgGraphics = Graphics.FromImage(image);
            imgGraphics.CopyFromScreen(x, y, 0, 0, new Size(width, height));
            string name = @"c:\sn.jpg";
            if (File.Exists(name))
            {
                File.Delete(name);
            }

            //保存
            //image.Save(name, ImageFormat.Jpeg);
            imgGraphics.Dispose();
            return image;
        }
    }
}

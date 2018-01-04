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

    class ScreenSnapUtil
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

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool SwitchToThisWindow(IntPtr hWnd, bool fAltTab);

        public void test()
        {
            IntPtr hWnd = FindWindow(null, "ecg.txt - 记事本");
            //ShowWindow(hWnd, 1);
            SwitchToThisWindow(hWnd, true);

            RECT rc = new RECT();
            GetWindowRect(hWnd, ref rc);
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
            image.Save(name, ImageFormat.Jpeg);
            imgGraphics.Dispose();
        }
    }
}

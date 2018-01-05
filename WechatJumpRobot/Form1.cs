using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace WechatJumpRobot
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Point p = ImageUtil.GetNextCenter(@"\\VBOXSVR\mayonglei\Desktop\RDPShare\微信跳一跳截图\IMG_1265.PNG");
            Console.WriteLine("center:{0}", p);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap bmp = ScreenSnapUtil.Snap("");
            if (null != bmp)
            {
                bmp.Save(@"C:\Users\MYL\Desktop\Jumpnn.jpg", ImageFormat.Jpeg);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.timer1.Enabled = true;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string file = @"C:\Users\MYL\Desktop\Jumpnn1.jpg";
            using (Bitmap bmp = ScreenSnapUtil.Snap(""))
            {
                if (null == bmp)
                {
                    return;
                }
                Point p = ImageUtil.GetNextCenter(bmp);
                if (p.X < 0 || p.Y < 0)
                {
                    return;
                }
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    Pen pen = new Pen(Color.Red);
                    g.DrawEllipse(pen, p.X, p.Y, 20, 20);
                }
                //this.pictureBox1.Image = null;
                //bmp.Save(file, ImageFormat.Jpeg);
                //this.pictureBox1.Image = Image.FromFile(file);
                this.pictureBox1.Image = (Image)bmp.Clone();
                Console.WriteLine(p);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string[] _files = Directory.GetFiles(@"\\VBOXSVR\mayonglei\Desktop\RDPShare\微信跳一跳截图", "*.png");
            for (int i = 0; i < _files.Length; i++)
            {
                using (Bitmap bmp = new Bitmap(_files[i]))
                {
                    if (null == bmp)
                    {
                        return;
                    }
                    Point p = ImageUtil.GetNextCenter(bmp);
                    if (p.X < 0 || p.Y < 0)
                    {
                        return;
                    }
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        Pen pen = new Pen(Color.Red);
                        g.DrawEllipse(pen, p.X, p.Y, 20, 20);
                    }
                    string _newFile = Directory.GetParent(_files[i]) + @"\new_" + Path.GetFileName(_files[i]);
                    bmp.Save(_newFile, ImageFormat.Png);
                }
            }
            Console.WriteLine("finish");
        }
    }
}

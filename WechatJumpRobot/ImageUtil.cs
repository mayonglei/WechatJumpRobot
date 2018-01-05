using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace WechatJumpRobot
{
    static class ImageUtil
    {
        public static Point GetNextCenter(Bitmap _bmp)
        {
            int _cBack = -1;
            Point? _p1 = null, _p2 = null, _p3 = null;
            int yMin = _bmp.Height / 4;
            for (int y = yMin; y < _bmp.Height; y++)
            {
                for (int x = 0; x < _bmp.Width; x++)
                {
                    Color _color = _bmp.GetPixel(x, y);
                    int _avg = (_color.R + _color.G + _color.B) / 3;
                    if (_cBack < 0)
                    {
                        _cBack = _avg;
                    }
                    if (Math.Abs(_avg - _cBack) > 10 && null == _p1)
                    {
                        Console.WriteLine("{0},{1}", _cBack, _avg);
                        _p1 = new Point(x, y);
                    }
                    if (Math.Abs(_avg - 145) <= 5 && IsGray(_color) && null == _p2)
                    {
                        Console.WriteLine("{0}", _avg);
                        _p2 = new Point(x, y);
                    }
                    if (Math.Abs(_avg - _cBack) <= 10 && null != _p2 && null == _p3)
                    {
                        Console.WriteLine("{0},{1}", _cBack, _avg);
                        _p3 = new Point(x, y);
                    }
                    //Console.WriteLine(_avg);
                }
            }
            if (null != _p1)
            {
                Console.WriteLine("p1:{0}", _p1.Value);
            }
            if (null != _p2)
            {
                Console.WriteLine("p2:{0}", _p2.Value);
            }
            if (null != _p3)
            {
                Console.WriteLine("p3:{0}", _p3.Value);
            }

            if (null != _p1 && null != _p2 && null != _p3)
            {
                if (Math.Abs((_p2.Value.X + _p3.Value.X) / 2 - _p1.Value.X) <= 20)
                {
                    return new Point((_p2.Value.X + _p3.Value.X) / 2, (_p2.Value.Y + _p3.Value.Y) / 2);
                }
                else
                {
                    return new Point(_p1.Value.X, (_p2.Value.Y + _p3.Value.Y) / 2);
                }

            }
            return new Point(-1, -1);
        }
        public static Point GetNextCenter(string ImageFile)
        {
            using (Bitmap _bmp = new Bitmap(ImageFile))
            {
                return GetNextCenter(_bmp);
            }

        }

        private static bool IsGray(Color c)
        {
            int _avg = (c.R + c.G + c.B) / 3;
            return Math.Abs(c.R - _avg) <= 10 && Math.Abs(c.G - _avg) <= 10 && Math.Abs(c.B - _avg) <= 10;
        }
    }
}

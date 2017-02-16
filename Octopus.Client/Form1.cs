using Octopus.Client.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Octopus.Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Bitmap bt = new Bitmap(100, 100);
            //Graphics g = Graphics.FromImage(bt);
            //Rectangle rect = new Rectangle(new Point(10, 10), new Size(30, 30));
            //Pen p = new Pen(Color.Red);
            //p.Width = 5;
            //g.DrawEllipse(p, rect);
            //pictureBox1.Image = bt;
            //bt.Save("1.jpg");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //SetFormCircle();
        }

        private void cornerImage3_Click(object sender, EventArgs e)
        {

        }

        private void SetFormCircle()
        {
            int radian = 4; //圆弧角的比率，可以自己改变这个值看具体的效果
            int w = this.Width; //窗体宽
            int h = this.Height; //窗体高

            //对于矩形的窗体，要在一个角上画个弧度至少需要2个点，所以4个角需要至少8个点
            Point p1 = new Point(radian, 0);
            Point p2 = new Point(w - radian, 0);
            Point p3 = new Point(w, radian);
            Point p4 = new Point(w, h - radian);
            Point p5 = new Point(w - radian, h);
            Point p6 = new Point(radian, h);
            Point p7 = new Point(0, h - radian);
            Point p8 = new Point(0, radian);

            System.Drawing.Drawing2D.GraphicsPath shape = new System.Drawing.Drawing2D.GraphicsPath();

            Point[] p = new Point[] { p1, p2, p3, p4, p5, p6, p7, p8 };
            shape.AddPolygon(p);

            //将窗体的显示区域设为GraphicsPath的实例
            this.Region = new System.Drawing.Region(shape);
        }

        private void Type(Control sender, int p_1, double p_2)
        {
            GraphicsPath oPath = new GraphicsPath();
            oPath.AddClosedCurve(
                new Point[] {
            new Point(0, sender.Height / p_1),
            new Point(sender.Width / p_1, 0),
            new Point(sender.Width - sender.Width / p_1, 0),
            new Point(sender.Width, sender.Height / p_1),
            new Point(sender.Width, sender.Height - sender.Height / p_1),
            new Point(sender.Width - sender.Width / p_1, sender.Height),
            new Point(sender.Width / p_1, sender.Height),
            new Point(0, sender.Height - sender.Height / p_1) },(float)p_2);

            sender.Region = new Region(oPath);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            string filename = "icon.png";//如果不是png类型，须转换
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(Resources.f2deb48f8c5494eeb02d6e542df5e0fe99257e6b,100,100);
            for (int y = 0; y < 100; y++)
            {
                for (int x = 0; x < 100; x++)
                {
                    if ((x - 50) * (x - 50) + (y - 50) * (y - 50) > 50 * 50)
                    {
                        bitmap.SetPixel(x, y, System.Drawing.Color.FromArgb(0, 255, 255, 255));
                    }
                }
            }

            Graphics g = CreateGraphics();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.DrawImage(bitmap, new Point(50, 50));
            g.DrawEllipse(new Pen(Color.LightGray), 50, 50, 100, 100);
            g.Dispose();
        }

        private void redrawCornerImage1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxCornerImage1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void pictureBoxCornerImage1_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }


    public interface IInterface
    {

    }
    public class Person : IInterface
    {
        public string Name { get; set; }

        public string GetName()
        {
            return "";
        }
    }

    public class Animal : IInterface
    {
        public string Sex { get; set; }

        public string GetSex()
        {
            return "";
        }
    }
}

using Octopus.Client.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Octopus.Client
{
    public partial class GDICornerImage : PictureBox
    {
        public GDICornerImage()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);

            this.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Invalidate();
            //this.Image = Resources.f2deb48f8c5494eeb02d6e542df5e0fe99257e6b;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            Graphics g = CreateGraphics();
            RectangleF rec = this.ClientRectangle;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.Half;
            GraphicsPath gp = new GraphicsPath();
            gp.AddEllipse(rec.X, rec.Y, rec.Width - 3f, rec.Height - 3f);
            gp.CloseAllFigures();
            base.OnPaint(pe);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Invalidate();
        }

        private void GDICornerImage_Paint(object sender, PaintEventArgs e)
        {
            //Bitmap bitmap = new Bitmap(this.Image, 100, 100);
            //for (int y = 0; y < 100; y++)
            //{
            //    for (int x = 0; x < 100; x++)
            //    {
            //        if ((x - 50) * (x - 50) + (y - 50) * (y - 50) > 50 * 50)
            //        {
            //            bitmap.SetPixel(x, y, Color.FromArgb(0, 255, 255, 255));
            //        }
            //    }
            //}

            //Graphics g = e.Graphics;//CreateGraphics();
            //g.SmoothingMode = SmoothingMode.AntiAlias;
            //g.DrawImage(bitmap, new Point(0, 0));
            //g.DrawEllipse(new Pen(Color.LightGray), 0, 0, 100, 100);
            //g.Dispose();
        }

        private void GDICornerImage_Move(object sender, EventArgs e)
        {
            //this.Invalidate();
        }
    }
}
